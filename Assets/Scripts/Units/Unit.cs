using System;
using System.Collections;
using System.Collections.Generic;
using Bryndzaky.Combat.Weapons;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Bryndzaky.Units {
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField]
        protected float moveSpeed = 3;
        [SerializeField]
        protected int maxHealth = 100;
        protected int health;
        [SerializeField]
        protected Rigidbody2D rb;
        [SerializeField]
        protected GameObject weaponObject;
        protected bool frozen = false;
        protected IWeapon weapon {
            get {
                return this.weaponObject != null ? this.weaponObject.GetComponent<IWeapon>() : null;
            }
        }
        protected Vector2 moveDirection;
        [SerializeField]
        protected Animator animator;
        private UnityEvent OnBegin, OnDone;
        private bool canMove = true;
        protected bool retreat = false;
        
        protected virtual void Start()
        {
            this.health = this.maxHealth;
            //this.weapon = WeaponManager.Instance.GetWeapon();
        }

        public void GrantWeapon(GameObject newWeapon)
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
            this.health -= damage;
            
            if (this.health <= 0)
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
            return this.health;
        }
        public void Heal(int healing)
        {   
            if(this.health != this.maxHealth)
                animator.SetTrigger("Heal");

            this.health = Math.Min(this.health + healing, this.maxHealth);
        }
        protected abstract void Die();   
    }
}