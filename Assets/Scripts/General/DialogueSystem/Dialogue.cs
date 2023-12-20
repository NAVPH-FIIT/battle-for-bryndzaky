using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Bryndzaky.General.Common;

[System.Serializable]
[CreateAssetMenu(fileName = "NewDialogueData", menuName = "Dialogue Data", order = 1)]
public class Dialogue : ScriptableObject
{
    public DialogueEntry[] dialogueEntries;
    public new string name;
    public Sprite characterImage;
}

[System.Serializable]
public struct DialogueEntry
{
    public bool IsHeroTalking;
    public string Sentence;
    
}
