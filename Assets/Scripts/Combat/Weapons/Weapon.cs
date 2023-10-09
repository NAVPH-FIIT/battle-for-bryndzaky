using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Bryndzaky.Weapons
{
    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        [SerializeField]
        protected int damage = 10;
        //protected bool attacking = false;
        protected bool canAttack = true;
        protected float AttackTime = 0.25f;
        [SerializeField]
        protected float cooldown = 1;
        [SerializeField]
        protected Animator weaponAnimation;

        public string GetHolder()
        {
            return transform.parent.gameObject.tag;
        }

        public void Aim()
        {
            SpriteRenderer characterSprite = gameObject.GetComponentInParent<SpriteRenderer>();
            SpriteRenderer weaponSprite = gameObject.GetComponent<SpriteRenderer>();

            Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
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
            StartCoroutine(Attack());
            canAttack = false;
            StartCoroutine(Cooldown(cooldown, (bool result) => canAttack = result));
        }

        public abstract IEnumerator Attack();

        public IEnumerator Cooldown(float time, System.Action<bool> callback)
        {
            yield return new WaitForSeconds(time);
            callback(true);
        }
    }

    internal interface IWeapon
    {
        public void Aim();
        public void IssueAttack();
        public string GetHolder();
        public IEnumerator Attack();
        public IEnumerator Cooldown(float time, System.Action<bool> callback);
    }
}