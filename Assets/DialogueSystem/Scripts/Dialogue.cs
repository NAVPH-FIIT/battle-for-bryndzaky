using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "NewDialogueData", menuName = "Dialogue Data", order = 1)]
public class Dialogue : ScriptableObject
{
    public DialogueEntry[] dialogueEntries;
    public string name;
    public Sprite characterImage;
}

[System.Serializable]
public struct DialogueEntry
{
    public bool IsHeroTalking;
    public string Sentence;
}
