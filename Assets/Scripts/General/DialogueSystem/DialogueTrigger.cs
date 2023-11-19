using System.Collections;
using System.Collections.Generic;
using Bryndzaky.Units.Player;
using Bryndzaky.General.Common;
using UnityEngine;


public class DialogueTrigger : MonoBehaviour, IInteractable
{
  public Dialogue dialogue;
  public SpriteRenderer interactionHint;
  public SpriteRenderer legend;
  public Animator animator;
  public bool oneTimeDialogue;
  private bool hasDialogueBeenTriggered;

  private IDialogueAction interactableAction;



  public void Awake(){
      this.interactionHint.enabled = false;
      this.legend.enabled = false;
      hasDialogueBeenTriggered = false; 
      interactableAction = GetComponent<IDialogueAction>();
  }
  public void ExecuteAction()
  {
      if (oneTimeDialogue && hasDialogueBeenTriggered)
      {
          // If it's a one-time dialogue and has already been triggered, do nothing
          return;
      }

      animator.SetTrigger("KeyDown");
      FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
      Debug.Log("Dialogue triggered");

      if (interactableAction != null)
      {
          Debug.Log("Interacting with");
          interactableAction.ExecuteAction();
      }
      else {
        Debug.Log("aweqeqw with");
      }

      if (oneTimeDialogue)
      {
          hasDialogueBeenTriggered = true; // Set the flag
      }
  }
  
  public void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player") && (!oneTimeDialogue || !hasDialogueBeenTriggered))
    {
      Debug.Log("Player has collided with the Interactable object!");
      this.interactionHint.enabled = true;
      this.legend.enabled = true;
      Debug.Log("Object is: " + this.ToString());
      other.GetComponent<Player>().possibleInteraction = this;
    }
  }

  public void OnTriggerExit2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      Debug.Log("Player has left the Interactable object!");
      this.interactionHint.enabled = false;
      this.legend.enabled = false;
      other.GetComponent<Player>().possibleInteraction = null;
    }
  }
}
