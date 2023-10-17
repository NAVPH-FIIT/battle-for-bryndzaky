using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Bryndzaky.Combat.Spells
{
    public abstract class Spell : MonoBehaviour, ISpell
    {
        [SerializeField] protected int power;
        [SerializeField] protected float cooldown;
        [SerializeField] protected GameObject effect;

        [HideInInspector] public bool Available { get; protected set; } = true;

        public virtual void Cast()
        {
            this.PlayEffect();
            StartCoroutine(ISpell.Cooldown(this.cooldown, (bool result) => this.Available = result));
        }

        protected void PlayEffect()
        {
            // TODO: enable sprite renderer
            // TODO: set trigger to animation
        }
    }
}