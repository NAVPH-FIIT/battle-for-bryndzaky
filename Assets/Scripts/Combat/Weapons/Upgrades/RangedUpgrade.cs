using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bryndzaky.Combat.Weapons
{
    [CreateAssetMenu]
    public class RangedUpgrade: WeaponUpgrade
    {
        public float range;
        public bool burst;
        public float burstCooldown = 0.1f;
        public int burstSize = 5;
    }
}
