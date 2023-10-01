using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame.Interfaces;

public class InteractScript : MonoBehaviour
{
    public SpriteRenderer spriteToUnhide;   // Drag the sprite's SpriteRenderer here in the inspector

    private IInteractable interactableObject;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            Debug.Log("Player has collided with the Interactable object!");

            // Unhide the sprite
            if (spriteToUnhide != null)
                spriteToUnhide.enabled = true;
            interactableObject = other.GetComponent<IInteractable>();

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            Debug.Log("Player has left the Interactable object!");

            // Hide the sprite
            if (spriteToUnhide != null)
                spriteToUnhide.enabled = false;
            interactableObject = null;

        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactableObject != null)
        {
            interactableObject.ExecuteAction();
        }
    }
}
