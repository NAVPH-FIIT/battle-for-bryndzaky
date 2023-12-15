using System;
using System.Collections;
using System.Collections.Generic;
using Bryndzaky.Combat.Weapons;
using Bryndzaky.Units.Enemies;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Pathfinding;
using UnityEngine.UIElements;
using System.Xml.Serialization;

namespace Bryndzaky.Units
{
    public class Fox : MonoBehaviour
    {
        [SerializeField]
        protected float moveSpeed = 15;
        [SerializeField]
        protected int maxHealth = 100;
        public int Health { get; protected set; }
        [SerializeField]
        protected Rigidbody2D rb;
        [SerializeField]
        protected UnityEngine.UI.Slider healthbarSlider;
        protected UnityEngine.UI.Image healthbarImage;
        [SerializeField]
        protected GameObject blood;

        protected Vector2 moveDirection;
        [SerializeField] protected Animator animator;
        private UnityEvent OnBegin, OnDone;
        public bool dead = false;
        private bool playerAware = false;
        private Vector2 playerDirection;
        private bool canMove = true;
        private Seeker playerSeeker;
        private Path playerPath;
        private int currentWaypoint = 0;
        private float counter = 0;
        private bool flag = false;
        protected virtual void Start()
        {
            this.Health = this.maxHealth;
            this.healthbarSlider = GetComponentInChildren<UnityEngine.UI.Slider>();
            this.healthbarImage = transform
                .Find("Canvas")?
                .Find("Healthbar")?
                .Find("Fill Area")?
                .Find("Fill")?.GetComponent<UnityEngine.UI.Image>();
            animator.SetBool("Speed", false);
            this.playerSeeker = gameObject.GetComponent<Seeker>();
            this.InvokeRepeating("UpdatePath", 0f, .5f);
            //this.weapon = WeaponManager.Instance.GetWeapon();
        }

   
        public void Hit(int damage, Vector3 sourceDirection)
        {
            animator.SetTrigger("Hit");
            this.Health -= damage;
            Instantiate(blood, transform.position, transform.rotation);
            if (this.healthbarSlider != null)
            {
                this.healthbarSlider.value = (float)Health / maxHealth;
                if (this.healthbarSlider.value >= 0.75)
                    this.healthbarImage.color = new Color(32 / 255f, 255 / 255f, 0 / 255f, 255 / 255f);
                else if (this.healthbarSlider.value <= 0.25)
                    this.healthbarImage.color = new Color(255 / 255f, 0 / 255f, 5 / 255f, 255 / 255f);
                else
                    this.healthbarImage.color = new Color(255 / 255f, 146 / 255f, 0 / 255f, 255 / 255f);
            }


            if (this.Health <= 0)
                this.Die();
        }

        protected void Update() {
            if (PauseManager.IsPaused)
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
        }

        private void FixedUpdate()
        {
            counter += Time.deltaTime;
            //Debug.LogWarning(Time.deltaTime);
            //Debug.LogWarning(counter);
            if (counter > 10 && counter < 15)
            {
                if (!flag)
                    moveDirection = new Vector2(UnityEngine.Random.Range(-100, 100), UnityEngine.Random.Range(-100, 100)).normalized;
                flag = true;
                rb.velocity = moveSpeed * moveDirection;
                //Debug.LogWarning(moveDirection);
            }
            else
                if (canMove && !PauseManager.IsPaused)
                    Move();

            if (counter>=15)
                counter = 0;
                flag = false;
            
        }

        protected void Animate() {
            if (rb.velocity != Vector2.zero || playerAware)
            {
                animator.SetFloat("Horizontal", playerDirection.x * -1);
                animator.SetFloat("Vertical", playerDirection.y * -1);
                //animator.SetFloat("Speed", new Vector2(playerDirection.x, playerDirection.y).sqrMagnitude);
                animator.SetBool("Speed", true);
            }
            else
            {
                animator.SetFloat("Horizontal", 0);
                animator.SetFloat("Vertical", 0);
                animator.SetBool("Speed", false);
            }
        }

        protected virtual void Move()
        {
            // if (gameObject.CompareTag("Enemy_Drab"))
            rb.velocity =  moveSpeed * moveDirection;// new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        }

        private void KnockBack(Vector3 sourceDirection)
        {
            canMove = false;
            StopAllCoroutines();
            OnBegin?.Invoke();
            Vector2 direction = (transform.position - sourceDirection).normalized;
            rb.AddForce(direction * (float)ConfigManager.config["combat.knockback.strength"], ForceMode2D.Impulse);
            StartCoroutine(KnockbackReset());
            ResetCooldown();
        }
        private IEnumerator KnockbackReset()
        {
            yield return new WaitForSeconds((float)ConfigManager.config["combat.knockback.delay"]);
            rb.velocity = Vector2.zero;
            canMove = true;
            OnDone?.Invoke();
        }

        protected virtual void ResetCooldown()
        {
            return;
        }

        public int GetHealth()
        {
            return this.Health;
        }

        public int GetmaxHealth()
        {
            return this.maxHealth;
        }
        public void Heal(int healing)
        {
            if (this.Health != this.maxHealth)
                animator.SetTrigger("Heal");

            this.Health = Math.Min(this.Health + healing, this.maxHealth);
        }
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

        protected void AssignDirection()
        {
            if (playerPath == null)
            {
                //moveDirection = Vector2.zero;
                return;
            }

            if (currentWaypoint >= playerPath.vectorPath.Count)
                return;
            // int choice = UnityEngine.Random.Range(-1, 1);
            moveDirection = ((Vector2)playerPath.vectorPath[currentWaypoint] - rb.position).normalized * -1;


            if (Vector2.Distance(rb.position, playerPath.vectorPath[currentWaypoint]) < 1)
            {
                currentWaypoint++;
            }
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
        protected void Die() {
            this.transform.GetComponentInChildren<Canvas>()?.gameObject.SetActive(false);

            foreach (IDeathrattle deathrattle in GetComponents<IDeathrattle>())
                deathrattle.GrantReward();

            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
            this.animator.SetTrigger("Dead");
        }
    }
}