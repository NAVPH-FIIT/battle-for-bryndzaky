using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Bryndzaky.Combat.Weapons
{
    public class WeaponManager : MonoBehaviour
    {
        public static WeaponManager Instance { get; private set; }

        public GameObject sword;
        public GameObject musket;

        void Start() {
            Instance = this;
        }

        public IWeapon GetSword()
        {
            return sword.GetComponent<IWeapon>();
        }

        public IWeapon GetMusket()
        {
            return musket.GetComponent<IWeapon>();
        }
    }
}