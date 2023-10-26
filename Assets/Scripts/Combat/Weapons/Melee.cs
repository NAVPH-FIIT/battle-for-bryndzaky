using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;

namespace Bryndzaky.Combat.Weapons
{
    public class Melee : Weapon, ICombatCollision
    {
        private bool alreadyAttacked = false;
        
        override
        protected void Start()
        {
            base.Start();
            gameObject.GetComponentInChildren<Collider2D>().enabled = false;
        }

        override
        public IEnumerator Attack()
        {
            weaponAnimation?.SetTrigger("Attack");
            gameObject.GetComponentInChildren<Collider2D>().enabled = true;
            yield return new WaitForSeconds(AttackTime);
            gameObject.GetComponentInChildren<Collider2D>().enabled = false;
            alreadyAttacked = false;
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            // Debug.Log("Attack, " + this.GetHolder());
            if (other.tag.StartsWith("Boss_") && this.GetHolder() == "Player")
            {
                BossHealth health = other.GetComponent<BossHealth>();
                health.Damage(damage);
                return;
            }
            string target = this.GetHolder() == "Player" ? "Enemy" : "Player";
            if (other.tag.Split('_')[0] == target && !alreadyAttacked)
            {
                Debug.Log("Hit, " + this.GetHolder());
                alreadyAttacked = true;
                other.GetComponent<Units.Unit>().Hit(damage, gameObject.transform.position);
            }
        }
    }
}