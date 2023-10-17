using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Bryndzaky.Combat.Spells
{
    public class Fireball : Spell
    {
        public override void Cast()
        {
            this.Available = false;

            // TODO: Fireball logic
            
            base.Cast();
        }
    }
}