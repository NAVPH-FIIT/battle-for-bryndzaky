using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bryndzaky.Units.Player;
using Bryndzaky.General.Common;

public class EnableLunchMeat : MonoBehaviour, IDialogueAction
{
    // Start is called before the first frame update
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


        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            // Check if the object's name is "LunchMeat"
            if (obj.name == "LunchMeat_Interaction")
            {
                // Activate the object
                obj.SetActive(true);
                Debug.Log("LunchMeat_Interaction object activated.");
                return; // Exit the loop once the object is found and activated
            }
        }

        Debug.LogWarning("No GameObject found with the name 'LunchMeat_Interaction'.");
    }
    
}
