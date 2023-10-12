using System.Collections;
using System.Collections.Generic;
using Bryndzaky.Combat.Collisions;
using Unity.VisualScripting;
using UnityEngine;

namespace Bryndzaky.Combat.Weapons
{
    public class Ranged : Weapon
    {
        [SerializeField]
        private float range;
        [SerializeField]
        private float burstCooldown = 0.1f;
        [SerializeField]
        private bool burst;
        [SerializeField]
        private int burstSize = 5;
        private int bulletCount = 0;
        public GameObject projectile;

        protected override void Start()
        {
            base.Start();
        }

        public override float GetCombatRange()
        {
            return range * 0.8f;
        }

        override
        public float GetCooldown()
        {
            if (burst && bulletCount++ <= burstSize)
            {
                bulletCount = 0;
                return burstCooldown;
            }
            
            return cooldown;
        }

        override
        public IEnumerator Attack()
        {
            yield return new WaitForSeconds(0);

            Instantiate(projectile, transform.position, transform.rotation).GetComponent<Projectile>().source = this.GetHolder();
            weaponAnimation?.SetTrigger("Fired");
        }
    }
}