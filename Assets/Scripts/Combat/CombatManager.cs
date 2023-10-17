using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Bryndzaky.Combat.Weapons;
using Bryndzaky.Combat.Spells;

namespace Bryndzaky.Combat
{
    public class CombatManager : MonoBehaviour
    {
        public static CombatManager Instance { get; private set; }
        
        [SerializeField]
        private List<GameObject> weapons;
        
        //[SerializeField]
        //sprivate List<GameObject> spells;
        
        void Start() {
            Instance = this;
        }

        public GameObject[] GetWeapons()
        {
            return this.weapons.ToArray();
        }

        public ISpell[] GetSpells()
        {
            Spell[] spells = gameObject.GetComponents<Spell>();
            for (int i = 0; i < spells.Length; i++)
                spells[i].Hotkey = KeyCode.Mouse3 + i;

            return spells;
        }
    }
}