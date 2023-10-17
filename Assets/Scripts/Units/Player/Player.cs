using System.Collections;
using System.Collections.Generic;
using Bryndzaky.General.Common;
using Unity.VisualScripting;
using UnityEngine;
using Bryndzaky.Combat.Weapons;

namespace Bryndzaky.Units.Player {
    public class Player : Unit
    {
        [HideInInspector]
        public static Player Instance { get; private set; }
        [HideInInspector]
        public IInteractable possibleInteraction = null;
        private bool isDashing = false;
        private bool canDash = true;
        [SerializeField] 
        private float dashSpeed = 10;
        [SerializeField] 
        private float dashDuration = 0.25f;
        [SerializeField] 
        public float dashCoolDown = 1f;

        protected override void Start() {
            base.Start();
            Instance = this;
            // this.weapon = WeaponManager.Instance.GetSword();
        }

        protected override void Update()
        {
            if (PauseManager.IsPaused)
                return;

            this.AssignDirection();
            this.Animate();
            
            this.weapon?.Aim(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
            this.PerformActions();
        }

        private void PerformActions()
        {
            if (Input.GetKeyDown(KeyCode.E) && this.possibleInteraction != null)
                this.possibleInteraction.ExecuteAction();

            if (Input.GetKey(KeyCode.Mouse0) && this.weapon != null)
                this.weapon.IssueAttack();

            if (Input.GetKeyDown(KeyCode.Space) && canDash)
            {
                canDash = false;
                StartCoroutine(Dash());
                StartCoroutine(IWeapon.Cooldown(dashCoolDown, (bool result) => canDash = result));
            }
        }

        protected override void Animate()
        {
            animator.SetFloat("Horizontal", moveDirection.x);
            animator.SetFloat("Vertical", moveDirection.y);
            animator.SetFloat("Speed", moveDirection.sqrMagnitude);
        }

        protected override void AssignDirection()
        {
            if (isDashing)
                return;

            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");
            moveDirection = new Vector2(moveX, moveY).normalized;
        }

        private IEnumerator Dash()
        {
            float defaultSpeed = moveSpeed;
            moveSpeed = dashSpeed;
            gameObject.GetComponent<Collider2D>().enabled = false;
            isDashing = true;
            yield return new WaitForSeconds(dashDuration);
            isDashing = false;
            gameObject.GetComponent<Collider2D>().enabled = true;
            moveSpeed = defaultSpeed;
        }

        public void GrantReward(int xp, int gold)
        {

        }

        protected override void Die()
        {
            Destroy(gameObject);
        }
    }
}