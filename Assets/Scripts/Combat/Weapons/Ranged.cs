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
        private int bulletCount = 1;
        public GameObject projectile;
        private Transform bulletSpawner;

        protected override void Start()
        {
            base.Start();
            bulletSpawner = GetComponentInChildren<Transform>().Find("BulletSpawner").GetComponent<Transform>();
        }

        public override float GetCombatRange()
        {
            return range * 0.8f;
        }

        override
        public float GetCooldown()
        {
            if (burst && (bulletCount++ < burstSize))
            { 
                return burstCooldown;
            }
            bulletCount = 1;
            return cooldown;
        }

        
        public override IEnumerator Attack()
        {
            yield return new WaitForSeconds(0);

            GameObject bullet = Instantiate(projectile, bulletSpawner.position, bulletSpawner.rotation);
            Projectile bullet_script = bullet.GetComponent<Projectile>();
            bullet_script.damage = damage;
            bullet.GetComponent<Projectile>().source = this.GetHolder();

            weaponAnimation?.SetTrigger("Fired");
        }

        public override void Initialize(WeaponUpgrade upgrade)
        {
            var rangedUpgrade = (RangedUpgrade) upgrade;
            
            this.damage = rangedUpgrade.damage;
            this.cooldown = rangedUpgrade.cooldown;
            this.range = rangedUpgrade.range;
            this.burst = rangedUpgrade.burst;
            this.burstCooldown = rangedUpgrade.burstCooldown;
            this.burstSize = rangedUpgrade.burstSize;
        }

    }
}