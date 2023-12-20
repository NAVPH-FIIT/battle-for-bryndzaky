using Bryndzaky.Combat;
using Bryndzaky.Units;
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
        [HideInInspector]
        public int damage = 10;
        [SerializeField]
        private float speed = 10f;
        [HideInInspector]
        public int freezingTime = 10;
        public GameObject hitEffect;
        [HideInInspector]
        public string source;

        protected virtual void Update()
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

        public virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag.StartsWith("Boss_") && source == "Player")
            {
                BossHealth health = other.GetComponent<BossHealth>();
                health.Damage(damage);
                this.PlayHitEffect();
                return;
            }
            if (other.tag.StartsWith("Special_") && source == "Player")
            {
                Debug.LogWarning("FoxHit");
                Fox fox = other.GetComponent<Fox>();
                fox.Hit(damage, gameObject.transform.position);
                this.PlayHitEffect();
                return;
            }
            string target = source == "Player" ? "Enemy" : "Player";
            if (other.tag.Split('_')[0] == target)
            {
                other.GetComponent<Units.Unit>().Hit(damage, gameObject.transform.position);
                this.PlayHitEffect();
            }
            
            if (other.CompareTag("Wall"))
                this.PlayHitEffect();
        }

        protected virtual void Awake()
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = speed * transform.right;
        }

        protected virtual void PlayHitEffect()
        {
            if (this.hitEffect == null)
                return;

            GameObject effect = Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(effect, 0.2f);
            Destroy(gameObject);
        }
    }
}
