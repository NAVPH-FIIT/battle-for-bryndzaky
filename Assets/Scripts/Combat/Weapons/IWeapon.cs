using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Bryndzaky.Combat.Weapons
{
    public interface IWeapon
    {
        public void Initialize(WeaponUpgrade upgrade);
        public void Aim(Vector2 dir);
        public void IssueAttack();
        public string GetHolder();
        public float GetCombatRange();
        public float GetCooldown();
        public IEnumerator Attack();
        public static IEnumerator Cooldown(float time, System.Action<bool> callback)
        {
            yield return new WaitForSeconds(time);
            callback(true);
        }
    }
}