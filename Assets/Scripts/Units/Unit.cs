using System;
using System.Collections;
using System.Collections.Generic;
using Bryndzaky.Combat.Weapons;
using Bryndzaky.Units.Enemies;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Bryndzaky.Units
{
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField]
        protected float moveSpeed = 3;
        [SerializeField]
        protected int maxHealth = 100;
        public int Health { get; protected set; }
        [SerializeField]
        protected Rigidbody2D rb;
        [SerializeField]
        protected GameObject weaponObject;
        protected bool frozen = false;
        protected Slider healthbarSlider;
        protected Image healthbarImage;
        protected IWeapon weapon
        {
            get
            {
                return this.weaponObject != null ? this.weaponObject.GetComponent<IWeapon>() : null;
            }
        }
        protected Vector2 moveDirection;
        [SerializeField] protected Animator animator;
        private UnityEvent OnBegin, OnDone;
        private bool canMove = true;
        protected bool retreat = false;

        protected virtual void Start()
        {
            this.Health = this.maxHealth;
            this.healthbarSlider = GetComponentInChildren<Slider>();
            this.healthbarImage = transform
                .Find("Canvas")?
                .Find("Healthbar")?
                .Find("Fill Area")?
                .Find("Fill")?.GetComponent<Image>();
            //this.weapon = WeaponManager.Instance.GetWeapon();
        }

        public void GrantWeapon(GameObject weaponPrefab)
        {
            if (this.weaponObject != null)
                Destroy(this.weaponObject);

            var newWeapon = Instantiate(weaponPrefab, transform.position, transform.rotation);//transform.position, transform.rotation);
            newWeapon.transform.localScale = transform.localScale;
            newWeapon.transform.SetParent(transform);

            this.weaponObject = newWeapon;
        }

        public void Hit(int damage, Vector3 sourceDirection)
        {
            animator.SetTrigger("Hit");
            this.KnockBack(sourceDirection);
            this.Health -= damage;
            if (this.healthbarSlider != null)
            {
                this.healthbarSlider.value = (float) Health / maxHealth;
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

        public void Freeze(int freezeTime)
        {
            frozen = true;
            canMove = false;
            StopAllCoroutines();
            rb.bodyType = RigidbodyType2D.Static;
            animator.SetBool("Freeze", true);
            StartCoroutine(FreezeCooldown(freezeTime));

        }

        private IEnumerator FreezeCooldown(int freezeTime)
        {
            yield return new WaitForSeconds(freezeTime);
            rb.bodyType = RigidbodyType2D.Dynamic;
            animator.SetBool("Freeze", false);
            canMove = true;
            frozen = false;
        }

        protected abstract void Update();

        private void FixedUpdate()
        {
            if (canMove && !PauseManager.IsPaused)
                Move();
        }

        protected abstract void Animate();

        protected abstract void AssignDirection();

        protected virtual void Move()
        {
            // if (gameObject.CompareTag("Enemy_Drab"))
            //     Debug.Log("kokot");
            rb.velocity = (retreat ? 0.5f : 1f) * moveSpeed * moveDirection;// new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        }

        private void KnockBack(Vector3 sourceDirection)
        {
            canMove = false;
            StopAllCoroutines();
            OnBegin?.Invoke();
            Vector2 direction = (transform.position - sourceDirection).normalized;
            rb.AddForce(direction * (float)ConfigManager.config["combat.knockback.strength"], ForceMode2D.Impulse);
            StartCoroutine(KnockbackReset());
        }
        private IEnumerator KnockbackReset()
        {
            yield return new WaitForSeconds((float)ConfigManager.config["combat.knockback.delay"]);
            rb.velocity = Vector2.zero;
            canMove = true;
            OnDone?.Invoke();
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
        protected abstract void Die();
    }
}