using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Bryndzaky.Combat.Collisions
{
    public class Projectile : MonoBehaviour, ICombatCollision
    {
        private float timer = 0;
        private int stopProjectile = 2;
        public int damage = 10;
        [SerializeField]
        private float speed = 10f;
        [SerializeField]
        private GameObject hitEffect;
        [HideInInspector]
        public string source;

        void Update()
        {
            if (timer < stopProjectile)
            {
                timer = timer + Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
                timer = 0;
            }
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            string target = source == "Player" ? "Enemy" : "Player";
            if (other.tag.Split('_')[0] == target)
            {
                other.GetComponent<Units.Unit>().Hit(damage, gameObject.transform.position);
                GameObject effect = Instantiate(hitEffect, transform.position, transform.rotation);
                Destroy(effect, 0.2f);
                Destroy(gameObject);
            }
            
            if (other.CompareTag("Wall"))
            {
                GameObject effect = Instantiate(hitEffect, transform.position, transform.rotation);
                Destroy(effect, 0.2f);
                Destroy(gameObject);
            }
        }

        public void Awake()
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = speed * transform.right;
        }
    }
}