using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogue : MonoBehaviour
{
  // Start is called before the first frame update
  public Dialogue dialogue;
  private bool hasDialogueBeenTriggered = false;
  void Start()
  {
    FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
  }

  public void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player") && !hasDialogueBeenTriggered)
    {
      FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
      hasDialogueBeenTriggered = true;
    }
  }
}
