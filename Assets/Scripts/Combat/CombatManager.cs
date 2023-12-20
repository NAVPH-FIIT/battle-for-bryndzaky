using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Bryndzaky.Combat.Weapons;
using Bryndzaky.Combat.Spells;
using System;
using System.Linq;
using Bryndzaky.General.Common;
using UnityEngine.SceneManagement;

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
        private List<ActiveWeapon> activeWeapons;
        public List<ActiveWeapon> ActiveWeapons
        { 
            get
            {
                return SceneManager.GetActiveScene().name == "Hub" ? this.BuildActiveWeapons() : this.activeWeapons;
            } 
        }

        //[SerializeField]
        //sprivate List<GameObject> spells;
        
        void Start() {
            Instance = this;

            this.activeWeapons = this.BuildActiveWeapons();
        }

        private List<ActiveWeapon> BuildActiveWeapons()
        {
            return StateManager.State.activeWeapons.Any()
            ? this.armory.allWeapons
                .Where(entry => StateManager.State.activeWeapons.Contains(entry.name))
                .Select(entry => new ActiveWeapon(
                    entry.name,
                    entry.prefab,
                    entry.upgrades[StateManager.State.GetWeaponGrade(entry.name)]
                ))
                .Take(4).ToList()
            :  this.armory.allWeapons
                .Select(entry => new ActiveWeapon(
                    entry.name,
                    entry.prefab,
                    entry.upgrades[0]
                ))
                .Take(4).ToList();
        }

        public ISpell[] GetSpells()
        {
            Spell[] spells = gameObject.GetComponents<Spell>();
            for (int i = 0; i < spells.Length; i++)
                spells[i].Hotkey = (1 + i).ToString();

            return spells;
        }
    }
}