using System.Collections;
using System.Collections.Generic;
using Bryndzaky.General.Common;
using Unity.VisualScripting;
using UnityEngine;
using Bryndzaky.Combat.Weapons;
using Bryndzaky.Units.Player;
using Pathfinding;
using Bryndzaky.Combat.Spells;
using Bryndzaky.Combat;
using System.Linq;

namespace Bryndzaky.Units.Laszlo
{
    public partial class Laszlo : Unit
    {
        public static Laszlo Instance { get; private set; }
        private Vector2 playerDirection;
        public float minimumDistance = 1;
        private List<ISpell> spells;
        [SerializeField]
        private int tresholdDistance = 200;

        protected override void Start()
        {
            base.Start();

            Instance = this;
            spells = CombatManager.Instance.GetSpells().ToList();
            animator.SetBool("Speed", false);
            this.playerSeeker = gameObject.GetComponent<Seeker>();
            this.InvokeRepeating("UpdatePath", 0f, .5f);
        }

        protected override void Update()
        {
            if (PauseManager.IsPaused)
            {
                moveDirection = Vector2.zero;
                return;
            }
            this.AssignDirection();
            this.Animate();
            
            this.playerDirection = Player.Player.Instance.gameObject.transform.position - transform.position;
            if (playerDirection.sqrMagnitude > tresholdDistance)
                Teleport();
            this.weapon?.Aim(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
            this.CastSpells();
        }

        private void CastSpells()
        {
            foreach (ISpell spell in spells)
                if (Input.GetKey(spell.Hotkey) && spell.Available)
                    ((Staff) this.weapon).CastSpell(spell);
        }

        protected override void Move() 
        {
            rb.velocity = moveSpeed * moveDirection;
        }
        protected override void Animate()
        {
            if (rb.velocity != Vector2.zero)
            {
                animator.SetFloat("Horizontal", playerDirection.x * (retreat ? -1 : 1));
                animator.SetFloat("Vertical", playerDirection.y * (retreat ? -1 : 1));
                //animator.SetFloat("Speed", new Vector2(playerDirection.x, playerDirection.y).sqrMagnitude);
                animator.SetBool("Speed", true);
            }
            else
            {
                animator.SetFloat("Horizontal", 0);
                animator.SetFloat("Vertical", 0);
                //animator.SetFloat("Speed", 0);
                animator.SetBool("Speed", false);
            }
        }

        protected override void Die()
        {
        }
       
    }
    public partial class Laszlo
    {
        private Seeker playerSeeker;
        private Path playerPath;
        private int currentWaypoint = 0;

        private void UpdatePath()
        {
            if (playerSeeker.IsDone())
                playerSeeker.StartPath(rb.position, Player.Player.Instance.transform.position, OnPathComplete);
        }

        private void OnPathComplete(Path path)
        {
            if (!path.error)
            {
                playerPath = path;
                currentWaypoint = 0;
            }
        }
        protected void Teleport()
        {
            this.transform.position = Player.Player.Instance.transform.position;
            animator.SetTrigger("Teleport");
        }
        protected override void AssignDirection()
        {
            if (playerPath == null)
            {
                //moveDirection = Vector2.zero;
                return;
            }

            if (currentWaypoint >= playerPath.vectorPath.Count)
                return;


            if (playerDirection.sqrMagnitude >= minimumDistance - 1 &&
                playerDirection.sqrMagnitude <= minimumDistance + 1)
            {
                moveDirection = Vector2.zero;
                retreat = false;
                return;
            }

            retreat = playerDirection.sqrMagnitude < (minimumDistance - 1);


            moveDirection = ((Vector2)playerPath.vectorPath[currentWaypoint] - rb.position).normalized * (retreat ? -1 : 1);

            if (Vector2.Distance(rb.position, playerPath.vectorPath[currentWaypoint]) < 1)
            {
                currentWaypoint++;
            }
        }
    }
}
