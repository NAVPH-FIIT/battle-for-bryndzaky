using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bryndzaky.Units.Player;
using Bryndzaky.General.Common;

public class InteractableAction : ScriptableObject, IDialogueAction
{
    public void ExecuteAction()
    {
        Debug.Log("InteractableAction Kokotkotkotktoktko");

        // Find all GameObjects with the tag "Tutorial_First"
        GameObject[] tutorialObjects = GameObject.FindGameObjectsWithTag("Tutorial_First");

        // Loop through each GameObject and disable it
        foreach (GameObject tutorialObject in tutorialObjects)
        {
            tutorialObject.SetActive(false);
        }
    }
}