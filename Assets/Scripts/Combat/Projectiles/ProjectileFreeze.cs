using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Bryndzaky.Combat.Collisions
{
    public class ProjectileFreeze : ProjectileAbstract
    {
        public override void OnTriggerEnter2D(Collider2D other)
        {
            string target = source == "Player" ? "Enemy" : "Player";
            if (other.tag.Split('_')[0] == target)
            {
                other.GetComponent<Units.Unit>().Freeze(freezingTime);
                this.PlayHitEffect();
            }

            if (other.CompareTag("Wall"))
                this.PlayHitEffect();
        }
    }
}