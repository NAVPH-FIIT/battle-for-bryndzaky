using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Bryndzaky.Combat.Spells
{
    public abstract class Spell : MonoBehaviour, ISpell
    {
        [SerializeField] protected int strength;
        [SerializeField] protected float cooldown;
        [SerializeField] protected GameObject effect;

        public bool Available { get; set; } = true;
        public string Hotkey { get; set; }
        public Transform Origin { get; set; }
        public GameObject Effect => effect;

        public abstract void Cast();

        // protected void PlayEffect()
        // {
        //     // TODO: enable sprite renderer
        //     // TODO: set trigger to animation
        // }
    }
}