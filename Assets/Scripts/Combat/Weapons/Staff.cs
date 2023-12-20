using System;
using System.Collections;
using System.Collections.Generic;
using Bryndzaky.Combat.Collisions;
using Bryndzaky.Combat.Spells;
using Unity.VisualScripting;
using UnityEngine;

namespace Bryndzaky.Combat.Weapons
{
    public class Staff : Weapon
    {
        public override IEnumerator Attack()
        {
            throw new System.NotImplementedException();
        }

        public override void Initialize(WeaponUpgrade upgrade)
        {
            throw new System.NotImplementedException();
        }

        public void CastSpell(ISpell spell)
        {
            this.PlayEffect(spell.Effect);

            spell.Origin = this.transform;
            spell.Cast();

            StartCoroutine(ISpell.Cooldown(this.cooldown, (bool result) => spell.Available = result));
        }

        private void PlayEffect(GameObject effect)
        {
            if (effect == null)
                return;
        }
    }
}