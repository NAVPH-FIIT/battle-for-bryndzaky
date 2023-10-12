using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Bryndzaky.Combat
{
    public interface ICombatCollision
    {
        public void OnTriggerEnter2D(Collider2D other);
    }
}