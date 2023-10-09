using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Bryndzaky.Units {
    public abstract class Unit : MonoBehaviour
    {
        public float moveSpeed = 10;
        public int health = 100;

        public Rigidbody2D rb;
        public Vector2 moveDirection;
        public Animator animator;
    
        public void TakeDamage(int damage) {
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

        private void Die()
        {
            Destroy(gameObject);
        }   
    }
}