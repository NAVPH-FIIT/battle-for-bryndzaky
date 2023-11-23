using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;

namespace Bryndzaky.Combat.Weapons
{
    public class Melee : Weapon, ICombatCollision
    {
        [SerializeField] private float attackTime = 0.25f;
        private bool alreadyAttacked = false;
        
        override
        protected void Start()
        {
            base.Start();
            gameObject.GetComponentInChildren<Collider2D>().enabled = false;
        }

        public override IEnumerator Attack()
        {
            weaponAnimation?.SetTrigger("Attack");
            gameObject.GetComponentInChildren<Collider2D>().enabled = true;
            yield return new WaitForSeconds(attackTime);
            gameObject.GetComponentInChildren<Collider2D>().enabled = false;
            alreadyAttacked = false;
        }

        public override void Initialize(WeaponUpgrade upgrade)
        {
            var meleeUpgrade = (MeleeUpgrade) upgrade;
            
            this.damage = meleeUpgrade.damage;
            this.cooldown = meleeUpgrade.cooldown;
            this.attackTime = meleeUpgrade.AttackTime;
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
