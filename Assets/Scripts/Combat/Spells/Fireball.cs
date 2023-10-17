using System;
using System.Collections;
using System.Collections.Generic;
using Bryndzaky.Combat.Collisions;
using Unity.VisualScripting;
using UnityEngine;

namespace Bryndzaky.Combat.Spells
{
    public class Fireball : Spell
    {
        [SerializeField] private GameObject projectile;

        public override void Cast()
        {
            this.Available = false;

            GameObject fireballObject = Instantiate(projectile, Origin.position, Origin.rotation);
            Projectile fireball = fireballObject.GetComponent<Projectile>();
            fireball.damage = this.strength;
            fireball.source = "Player";
        }
    }
}