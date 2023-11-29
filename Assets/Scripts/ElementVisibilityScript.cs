using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bryndzaky.Units.Player;
using Bryndzaky.General.Common;

public class ElementVisibility : MonoBehaviour, IDialogueAction
{
 
  public GameObject[] gameObjects; // GameObjects that will negate their current state.
  public void ExecuteAction()
  {

    if (gameObjects.Length > 0) {
        foreach (var obj in gameObjects) {
            if (obj != null) {
                obj.SetActive(!obj.activeSelf);
            }
        }
    }
  }

}
