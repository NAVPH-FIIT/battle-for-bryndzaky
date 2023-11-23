using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Bryndzaky.Combat.Weapons;
using Bryndzaky.Combat.Spells;
using System;
using System.Linq;

namespace Bryndzaky.Combat
{
    public class CombatManager : MonoBehaviour
    {
        public static CombatManager Instance { get; private set; }

        public class ActiveWeapon
        {
            public string name;
            public GameObject weaponPrefab;
            public WeaponUpgrade upgrade;

            public ActiveWeapon(string name, GameObject prefab, WeaponUpgrade upgrade)
            {
                this.name = name;
                this.weaponPrefab = prefab;
                this.upgrade = upgrade;
            }
        }
        
        [SerializeField] private Armory armory;
        public List<ActiveWeapon> activeWeapons { get; private set; }

        //[SerializeField]
        //sprivate List<GameObject> spells;
        
        void Start() {
            Instance = this;

            if (PlayerPrefs.GetString("ActiveWeapons") != "")
            {
                var chosenWeapons = PlayerPrefs.GetString("ActiveWeapons").Split("|");
                this.activeWeapons = this.armory.allWeapons
                    .Where(entry => chosenWeapons.Contains(entry.name))
                    .Select(entry => new ActiveWeapon(
                        entry.name,
                        entry.prefab,
                        entry.upgrades[PlayerPrefs.GetInt(entry.name, 0)]
                    ))
                    .Take(4).ToList();
            }
            else
                this.activeWeapons = this.armory.allWeapons
                    .Select(entry => new ActiveWeapon(
                        entry.name,
                        entry.prefab,
                        entry.upgrades[0]
                    ))
                    .Take(4).ToList();
        }

        // public GameObject[] GetWeapons()
        // {
        //     return this.weapons.ToArray();
        // }

        public ISpell[] GetSpells()
        {
            Spell[] spells = gameObject.GetComponents<Spell>();
            for (int i = 0; i < spells.Length; i++)
                spells[i].Hotkey = (1 + i).ToString();

            return spells;
        }
    }
}