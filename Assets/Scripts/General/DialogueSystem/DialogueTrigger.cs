using System.Collections;
using System.Collections.Generic;
using MyGame.Interfaces;
using UnityEngine;


public class DialogueTrigger : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    public Dialogue dialogue;

    public void ExecuteAction(){
      FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
