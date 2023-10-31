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


  public void Awake(){
      this.interactionHint.enabled = false;
      this.legend.enabled = false;
  }
  public void ExecuteAction(){
    animator.SetTrigger("KeyDown");
    FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
  }
  
  public void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
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
