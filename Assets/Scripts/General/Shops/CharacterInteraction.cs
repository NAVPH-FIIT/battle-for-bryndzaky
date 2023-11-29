using System.Collections;
using System.Collections.Generic;
using Bryndzaky.Units.Player;
using Bryndzaky.General.Common;
using UnityEngine;


public class CharacterInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject characterUI;
    [SerializeField] private SpriteRenderer interactionHint;
    [SerializeField] private SpriteRenderer legend;
    [SerializeField] private Animator animator;


  public void ExecuteAction(){
    this.animator.SetTrigger("KeyDown");
    this.characterUI.SetActive(true);
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
      this.characterUI.SetActive(false);
    }
  }
}
