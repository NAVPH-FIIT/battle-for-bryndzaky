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

        [Serializable]
        public class WeaponEntry
        {
            public string name;
            public GameObject prefab;
        }
        
        [SerializeField] private List<WeaponEntry> allWeapons;
        public List<WeaponEntry> activeWeapons { get; private set; }

        //[SerializeField]
        //sprivate List<GameObject> spells;
        
        void Start() {
            Instance = this;

            if (PlayerPrefs.GetString("ActiveWeapons") != "")
            {
                var chosenWeapons = PlayerPrefs.GetString("ActiveWeapons").Split("|");
                this.activeWeapons = allWeapons.Where(entry => chosenWeapons.Contains(entry.name)).Take(4).ToList();
            }
            else
                this.activeWeapons = allWeapons.Take(4).ToList();
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