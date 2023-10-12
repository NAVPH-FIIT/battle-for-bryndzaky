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
        protected int health = 100;
        [SerializeField]
        protected Rigidbody2D rb;
        [SerializeField]
        protected GameObject weaponObject;
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
            //this.weapon = WeaponManager.Instance.GetWeapon();
        }

        public void Hit(int damage, Vector3 sourceDirection) 
        {
            animator.SetTrigger("Hit");
            this.KnockBack(sourceDirection);
            this.health -= damage;
            
            if (this.health <= 0)
                this.Die();
        }

        protected virtual void Update()
        {
            this.AssignDirection();
            this.Animate();
        }

        private void FixedUpdate()
        {
            if (canMove && !PauseManager.IsPaused)
                Move();
        }

        protected abstract void Animate();

        protected abstract void AssignDirection();

        private void Move()
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

        protected abstract void Die();   
    }
}