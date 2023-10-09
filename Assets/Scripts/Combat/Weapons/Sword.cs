using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Bryndzaky.Weapons
{
    public class Sword : Weapon
    {
        private bool alreadyAttacked = false;
        void Start()
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
        }

        override
        public IEnumerator Attack()
        {
            weaponAnimation.SetTrigger("Attack");
            gameObject.GetComponent<Collider2D>().enabled = true;
            yield return new WaitForSeconds(AttackTime);
            gameObject.GetComponent<Collider2D>().enabled = false;
            alreadyAttacked = false;
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            string target = this.GetHolder() == "Player" ? "Enemy_" : "Player";
            if (other.tag.StartsWith(target) && !alreadyAttacked)
            {
                alreadyAttacked = true;
                other.GetComponent<Units.Unit>().Hit(damage, gameObject.transform.position);
            }
        }
    }
}