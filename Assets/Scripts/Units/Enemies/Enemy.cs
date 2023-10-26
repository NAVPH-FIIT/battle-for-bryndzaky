using System.Collections;
using System.Collections.Generic;
using Bryndzaky.General.Common;
using Unity.VisualScripting;
using UnityEngine;
using Bryndzaky.Combat.Weapons;
using Bryndzaky.Units.Player;
using Pathfinding;

namespace Bryndzaky.Units.Enemies 
{
    public partial class Enemy : Unit
    {
        private bool playerAware = false;
        private Vector2 playerDirection;
        public bool dead = false;
        

        protected override void Start()
        {
            base.Start();
            // this.weapon = WeaponManager.Instance.GetMusket();
            animator.SetBool("Speed", false);
            this.playerSeeker = gameObject.GetComponent<Seeker>();
            this.InvokeRepeating("UpdatePath", 0f, .5f);
        }

        protected override void Update()
        {
            if (PauseManager.IsPaused || frozen)
            {
                moveDirection = Vector2.zero;
                return;
            }

            this.Animate();
            
            if (!playerAware) 
            {
                moveDirection = Vector2.zero;
                return;
            }
            
            this.AssignDirection();

            this.playerDirection = Player.Player.Instance.gameObject.transform.position - transform.position;//().normalized;

            this.weapon?.Aim(playerDirection);
            this.weapon?.IssueAttack();
        }

        protected override void Animate()
        {
            //Debug.Log("Velocity: " + (rb.velocity != Vector2.zero).ToString());
            //Debug.Log("Aware: " + (playerAware).ToString());
            if (rb.velocity != Vector2.zero || playerAware)
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
            Player.Player.Instance.GrantReward(0,0);
            this.transform.GetComponentInChildren<Canvas>()?.gameObject.SetActive(false);
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
            this.animator.SetTrigger("Dead");
        }

        public void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag.Equals("Player"))
                playerAware = true;
        }

        public void OnTriggerStay2D(Collider2D collider)
        {
            if (collider.tag.Equals("Player"))
                playerAware = true;
        }

        public void OnTriggerExit2D(Collider2D collider)
        {
            playerAware = false;    
        }
    }

    public partial class Enemy
    {
        private Seeker playerSeeker;
        private Path playerPath;
        private int currentWaypoint = 0;

        private void UpdatePath()
        {
            if (playerAware && playerSeeker.IsDone())
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

        protected override void AssignDirection()
        {
            if (playerPath == null)
            {
                //moveDirection = Vector2.zero;
                return;
            }

            if (currentWaypoint >= playerPath.vectorPath.Count)
                return;

            if (weapon.GetCombatRange() > 0)
            {
                if (playerDirection.sqrMagnitude >= weapon.GetCombatRange() - 1 && 
                    playerDirection.sqrMagnitude <= weapon.GetCombatRange() + 1)
                {
                    moveDirection = Vector2.zero;
                    retreat = false;
                    return;
                }

                // Debug.Log(playerDirection.sqrMagnitude);
                // Debug.Log(weapon.GetCombatRange());
                retreat = playerDirection.sqrMagnitude < (weapon.GetCombatRange()-1);            
                // Debug.Log(retreat);
            }

            moveDirection = ((Vector2)playerPath.vectorPath[currentWaypoint] - rb.position).normalized * (retreat ? -1 : 1);
           
            if (Vector2.Distance(rb.position, playerPath.vectorPath[currentWaypoint]) < 1)
            {
                currentWaypoint++;
            }
        }
    }
}