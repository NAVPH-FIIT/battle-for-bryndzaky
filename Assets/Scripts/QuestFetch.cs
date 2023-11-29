using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bryndzaky.Units.Player;
using Bryndzaky.General.Common;

public class QuestFetch : MonoBehaviour, IDialogueAction
{
  public GameObject fetch_item;
  public GameObject waiting_dialogue;
  public GameObject[] doors; // Generally, NPC give you keys to doors where the fetch item is located. As we have an interaction when door is locked, we cannot simply enable the interaction, it needs to be present all the time.


  public void ExecuteAction()
  {
    fetch_item.SetActive(true);

    if(waiting_dialogue != null) {
      waiting_dialogue.SetActive(true);
    }



    if (doors.Length > 0) {
      foreach (var obj in doors) {
          if (obj != null) {
              DoorUnlockScript doorScript = obj.GetComponent<DoorUnlockScript>();
              if (doorScript != null) {
                  doorScript.unlocked = true;
              }
          }
      }
    }
  }

}
