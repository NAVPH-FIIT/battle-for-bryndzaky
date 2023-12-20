using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bryndzaky.Combat.Weapons
{
    public abstract class WeaponUpgrade : ScriptableObject
    {
        public int price;
        public int damage = 10;
        public float cooldown = 1;
    }
}
