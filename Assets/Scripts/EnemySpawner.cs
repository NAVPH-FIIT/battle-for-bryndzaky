using Bryndzaky.Combat.Weapons;
using Bryndzaky.Units;
using Bryndzaky.Units.Enemies;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : Spawner
{
    [Serializable]
    protected class Enemy: Entity
    {
        public GameObject weaponPrefab;
        public WeaponUpgrade weaponUpgrade;
        public int goldReward;
        public int xpReward;

        public override void Respawn()
        {
            base.Respawn();

            this.entityObject.GetComponent<Bryndzaky.Units.Enemies.Enemy>().GrantWeapon(weaponPrefab, weaponUpgrade);
        }
    }

    [SerializeField] private List<Enemy> enemies; 
}
