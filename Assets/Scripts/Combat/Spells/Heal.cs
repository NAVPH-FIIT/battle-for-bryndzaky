using System;
using System.Collections;
using System.Collections.Generic;
using Bryndzaky.Combat.Collisions;
using Bryndzaky.Units.Player;
using Unity.VisualScripting;
using UnityEngine;
namespace Bryndzaky.Combat.Spells
{
    public class Heal : Spell
    {
        public override void Cast()
        {
            this.Available = false;
            Player.Instance.Heal(this.strength);
        }
    }
}
