using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Bryndzaky.Combat.Spells
{
    public interface ISpell
    {
        public bool Available { get; set; }
        public string Hotkey { get; }
        public GameObject Effect { get; }
        public Transform Origin { get; set;}
        public void Cast();
        public static IEnumerator Cooldown(float time, System.Action<bool> callback)
            {
                yield return new WaitForSeconds(time);
                callback(true);
            }
    }
}