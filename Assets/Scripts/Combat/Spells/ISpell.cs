using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Bryndzaky.Combat.Spells
{
    public interface ISpell
    {
        public bool Available { get; }
        public void Cast();
        protected static IEnumerator Cooldown(float time, System.Action<bool> callback)
            {
                yield return new WaitForSeconds(time);
                callback(true);
            }
    }
}