using Bryndzaky.Combat.Weapons;
using Bryndzaky.Units.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : Spawner
{
    [Serializable]
    public class EnemySpawn: EntitySpawn
    {
        public GameObject weaponPrefab;
        public WeaponUpgrade weaponUpgrade;
        public EnemyStats enemyStats;

        public override void Respawn()
        {
            base.Respawn();

            var enemy = this.entityObject.GetComponent<Enemy>();
            enemy.GrantWeapon(this.weaponPrefab, this.weaponUpgrade);
            if (this.enemyStats != null)
                enemy.initialize(this.enemyStats);
        }
    }

    [SerializeField] private List<EnemySpawn> enemies;
    protected override List<IEntity> Entities { get{
        return this.enemies.Select(e => (IEntity)e).ToList();
    } }
}
