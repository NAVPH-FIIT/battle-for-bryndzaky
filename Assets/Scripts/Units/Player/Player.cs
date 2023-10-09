using System.Collections;
using System.Collections.Generic;
using Bryndzaky.General.Common;
using Bryndzaky.Weapons;
using Unity.VisualScripting;
using UnityEngine;

namespace Bryndzaky.Units.Player {
    public class Player : Unit
    {
        [HideInInspector]
        public static Player Instance { get; private set; }
        [HideInInspector]
        public IInteractable possibleInteraction = null;
        private IWeapon weapon;

        void Start() {
            Instance = this;
        }

        void Update()
        {
            if (PauseManager.IsPaused)
                return;
            
            this.AssignDirection();
            this.PerformActions();
        }

        private void PerformActions()
        {
            if (Input.GetKeyDown(KeyCode.E) && this.possibleInteraction != null)
                this.possibleInteraction.ExecuteAction();

            if (Input.GetKeyDown(KeyCode.Mouse0) && this.weapon != null)
                this.weapon.Attack();
        }

        private void AssignDirection()
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");
            moveDirection = new Vector2(moveX, moveY).normalized;

            animator.SetFloat("Horizontal", moveDirection.x);
            animator.SetFloat("Vertical", moveDirection.y);
            animator.SetFloat("Speed", moveDirection.sqrMagnitude);
        }

        override
        public void Die()
        {
            Destroy(gameObject);
        }
    }
}