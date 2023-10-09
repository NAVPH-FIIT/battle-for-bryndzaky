using System.Collections;
using System.Collections.Generic;
using Bryndzaky.General.Common;
using Unity.VisualScripting;
using UnityEngine;

namespace Bryndzaky.Units.Player {
    public class Player : Unit
    {
        public static Player Instance { get; private set; }
        public IInteractable possibleInteraction = null;

        void Update()
        {
            if (PauseManager.IsPaused)
                return;
            
            this.AssignDirection();
            this.Interact();
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

        private void Interact()
        {
            if (this.possibleInteraction != null && Input.GetKeyDown(KeyCode.E))
                this.possibleInteraction.ExecuteAction();
        }
    }
}