using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewTutorialData", menuName = "Tutorial Data", order = 1)]
public class TutorialData : ScriptableObject
{
    public string tutorialMessage;
    // Any other data you might need for each tutorial step.
}
