using System.Collections;
using System.Collections.Generic;
using Bryndzaky.Units.Player;
using Bryndzaky.General.Common;
using UnityEngine;


public class GrabInteraction : MonoBehaviour, IInteractable
{
  public SpriteRenderer interactionHint;
  public SpriteRenderer legend;

  public AudioClip grabSound;
  private AudioSource audioSource;

  public Animator animator;

  public void Awake() {
    this.legend.enabled = false;
    this.interactionHint.enabled = false;
    audioSource = GetComponent<AudioSource>();
    if (audioSource == null)
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }
  }

  public void ExecuteAction(){
    animator.SetTrigger("KeyDown");
    audioSource.PlayOneShot(grabSound);
    
    StartCoroutine(DeactivateAfterSound(grabSound.length));
    }
  private IEnumerator DeactivateAfterSound(float delay){
    // Wait for the length of the sound clip
    yield return new WaitForSeconds(delay);

    // Deactivate the parent object, or the current object if no parent is found
    if (this.transform.parent != null) {
        this.transform.parent.gameObject.SetActive(false);
    } else {
        Debug.LogWarning("No parent object found. Deactivating the current object instead.");
        this.gameObject.SetActive(false);
    }
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
