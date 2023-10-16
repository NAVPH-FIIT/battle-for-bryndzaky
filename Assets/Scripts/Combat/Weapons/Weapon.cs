using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Bryndzaky.Combat.Weapons
{
    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        [SerializeField]
        protected int damage = 10;
        //protected bool attacking = false;
        protected bool canAttack = true;
        [SerializeField]
        protected float AttackTime = 0.25f;
        [SerializeField]
        protected float cooldown = 1;
        protected Animator weaponAnimation;

        public string GetHolder()
        {
            return transform.parent.gameObject.tag;
        }

        public virtual float GetCombatRange()
        {
            return 0f;
        }

        public virtual float GetCooldown()
        {
            return this.cooldown;
        }

        protected virtual void Start()
        {
            this.weaponAnimation = GetComponentInChildren<Animator>();
        }   

        public void Aim(Vector2 dir)
        {
            if (gameObject.GetComponentInChildren<SpriteRenderer>() == null)
                return;

            SpriteRenderer characterSprite = gameObject.GetComponentInParent<SpriteRenderer>();
            SpriteRenderer weaponSprite = gameObject.GetComponentInChildren<SpriteRenderer>();

            // Vector2 dir = ;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotation;

            Vector2 scale = transform.localScale;
            scale.y = dir.x < 0 ? -1 : 1;

            transform.localScale = scale;

            weaponSprite.sortingOrder = dir.y > 0 ? characterSprite.sortingOrder - 1 : characterSprite.sortingOrder + 1;
        }

        public void IssueAttack()
        {
            if (canAttack)
            {
                StartCoroutine(Attack());
                canAttack = false;
                StartCoroutine(IWeapon.Cooldown(GetCooldown(), (bool result) => canAttack = result));
            }
        }

        public abstract IEnumerator Attack();

        // public IEnumerator Cooldown(float time, System.Action<bool> callback)
        // {
        //     yield return new WaitForSeconds(time);
        //     callback(true);
        // }
    }

    // internal interface IWeapon
    // {
    //     public void Aim();
    //     public void IssueAttack();
    //     public string GetHolder();
    //     public float GetCooldown();
    //     public IEnumerator Attack();
    //     public static IEnumerator Cooldown(float time, System.Action<bool> callback)
    //     {
    //         yield return new WaitForSeconds(time);
    //         callback(true);
    //     }
    // }
}