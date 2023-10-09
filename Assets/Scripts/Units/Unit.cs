using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Bryndzaky.Units {
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField]
        protected float moveSpeed = 10;
        [SerializeField]
        protected int health = 100;

        [SerializeField]
        protected Rigidbody2D rb;
        protected Vector2 moveDirection;
        [SerializeField]
        protected Animator animator;
        private UnityEvent OnBegin, OnDone;
        
        public void Hit(int damage, Vector3 sourceDirection) 
        {
            animator.SetTrigger("Hit");
            this.KnockBack(sourceDirection);
            this.health -= damage;
            
            if (this.health <= 0)
                this.Die();
        }

        private void FixedUpdate()
        {
            if (!PauseManager.IsPaused)
                Move();
        }

        private void Move()
        {
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        }

        private void KnockBack(Vector3 sourceDirection)
        {
            // UnityEvent OnBegin, OnDone;
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
            OnDone?.Invoke();
        }

        public abstract void Die();   
    }
}