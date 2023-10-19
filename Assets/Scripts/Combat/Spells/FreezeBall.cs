using System;
using System.Collections;
using System.Collections.Generic;
using Bryndzaky.Combat.Collisions;
using Unity.VisualScripting;
using UnityEngine;
namespace Bryndzaky.Combat.Spells
{
    public class FreezeBall : Spell
    {
        [SerializeField] private GameObject projectile;

        public override void Cast()
        {
            this.Available = false;

            GameObject freezeballObject = Instantiate(projectile, Origin.position, Origin.rotation);
            ProjectileFreeze freezeball = freezeballObject.GetComponent<ProjectileFreeze>();
            //freezeball.damage = this.strength;
            freezeball.freezingTime = this.strength;
            freezeball.source = "Player";
        }
    }
}   
