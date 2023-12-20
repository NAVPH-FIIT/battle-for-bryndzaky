using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bryndzaky.Combat.Weapons
{
    [CreateAssetMenu]
    public class Armory : ScriptableObject
    {
        [Serializable]
        public class WeaponEntry
        {
            public string name;
            public GameObject prefab;
            public List<WeaponUpgrade> upgrades;
        }

        public List<WeaponEntry> allWeapons;
    }
}
