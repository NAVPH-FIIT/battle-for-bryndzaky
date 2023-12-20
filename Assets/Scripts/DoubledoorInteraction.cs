using System.Collections;
using System.Collections.Generic;
using Bryndzaky.Units.Player;
using Bryndzaky.General.Common;
using UnityEngine;


public class DoorUnlockScript : MonoBehaviour, IInteractable
{
  public SpriteRenderer interactionHint;
  public SpriteRenderer legend;
  public Sprite legendUnlocked;
  public SpriteRenderer leftDoor;
  public SpriteRenderer rightDoor;
  public Sprite leftDoorOpen;
  public bool unlocked;


  public AudioClip doorSound;
  public AudioClip doorLockedSound;
  private AudioSource audioSource;
  public Sprite rightDoorOpen;
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
    if(this.unlocked) {
      this.leftDoor.sprite = this.leftDoorOpen;
      this.rightDoor.sprite = this.rightDoorOpen;

      audioSource.PlayOneShot(doorSound);

      RemoveBoxCollider(this.leftDoor);
      RemoveBoxCollider(this.rightDoor);

      StartCoroutine(DelayedDestroy(doorSound.length));

    }

    audioSource.PlayOneShot(doorLockedSound);
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

  public void Update() {
    if(this.unlocked) {
      this.legend.sprite = this.legendUnlocked;
      this.legend.color = new Color(255,255,255,255);
    }
  }

  private void RemoveBoxCollider(SpriteRenderer door)
  {
      BoxCollider2D collider = door.GetComponent<BoxCollider2D>();
      if (collider != null)
      {
          Destroy(collider);
      }
  }

  IEnumerator DelayedDestroy(float delay)
  {
    yield return new WaitForSeconds(delay);
    Destroy(this.gameObject);
  }

}
