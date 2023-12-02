using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
  public TextMeshProUGUI nameText;
  public TextMeshProUGUI dialogueText;

  public string heroName = "Ferko Veselý";
  public Image heroImage = null;

  private bool firstSentence = true;
  public Image characterImage;
  public Animator animator;

  private Queue<DialogueEntry> dialogueEntries;
  private string charName;

  private bool isDialogueInProgress = false;





  void Update()
  {
    if (isDialogueInProgress == true && Input.GetKeyDown(KeyCode.Return) && !PauseManager.IsPaused)
    {
      DisplayNextSentence();
    }
  }

  void Start()
  {
    dialogueEntries = new Queue<DialogueEntry>();
  }

  public void StartDialogue(Dialogue dialogue)
  {
    dialogueEntries.Clear();
    isDialogueInProgress = true;

    foreach (DialogueEntry entry in dialogue.dialogueEntries)
    {
      dialogueEntries.Enqueue(entry);
    }

    animator.SetBool("IsOpen", true);

    if (dialogue.characterImage != null)
    {
      characterImage.sprite = dialogue.characterImage;
    }

    if (dialogueEntries.Peek().IsHeroTalking)
    {
      nameText.text = heroName;
      heroImage.color = new Color(1f, 1f, 1f, 1f);  // Set the hero image color to fully opaque
      characterImage.color = new Color(1f, 1f, 1f, 0f); // Hide the other character's image
    }
    else
    {
      characterImage.color = new Color(0f, 0f, 0f, 1f); // Set the character image to fully opaque
      heroImage.color = new Color(1f, 1f, 1f, 0f); // Hide the hero's image
    }


    // dialogueText.text = "...";
    charName = dialogue.name;

    DisplayNextSentence();

  }

  public void DisplayNextSentence()
  {
    if (dialogueEntries.Count == 0)
    {
      EndDialogue();
      return;
    }

    bool heroTalks = dialogueEntries.Peek().IsHeroTalking;


    // ak hrdina zacne vypravat v strede dialogu
    if (heroTalks == true)
    {
      heroImage.color = new Color(1f, 1f, 1f, 1f);
      nameText.text = heroName;
      characterImage.color = new Color(1f, 1f, 1f, 0f);
    }
    else
    {
      nameText.text = charName;
      heroImage.color = new Color(1f, 1f, 1f, 0f);

      if (characterImage != null)
      {
        characterImage.color = new Color(1f, 1f, 1f, 1f);
      }
    }

    if (firstSentence == true)
    {
      if (heroTalks == false)
      {
        characterImage.color = new Color(1f, 1f, 1f, 1f); // Set alpha to 1 (fully opaque)
      }

      nameText.text = heroTalks == true ? heroName : charName;
      dialogueText.text = "";
      firstSentence = false;
    }


    DialogueEntry currentEntry = dialogueEntries.Dequeue();
    string sentence = currentEntry.Sentence;
    StopAllCoroutines();
    StartCoroutine(TypeSentence(sentence));
  }

  IEnumerator TypeSentence(string sentence)
  {
    dialogueText.text = "";
    foreach (char letter in sentence.ToCharArray())
    {
      dialogueText.text += letter;
      yield return new WaitForSeconds(0.01f);
    }
  }

  public void EndDialogue()
  {
    animator.SetBool("IsOpen", false);
    isDialogueInProgress = false;
    firstSentence = true;
    Debug.Log("EndDialogue");
  }
}