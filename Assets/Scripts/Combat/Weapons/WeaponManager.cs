using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Bryndzaky.Weapons
{
    public class WeaponManager : MonoBehaviour
    {
        public static WeaponManager Instance { get; private set; }
        public List<Weapon> weapons;

        void Start() {
            Instance = this;
        }
    }
}