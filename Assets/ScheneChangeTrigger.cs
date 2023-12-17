using System.Collections;
using System.Collections.Generic;
using Bryndzaky.General.Common;
using Unity.VisualScripting;
using UnityEngine;

public class ScheneChangeTrigger : MonoBehaviour
{
    [SerializeField] private string targetScene;
    [SerializeField] private string transitionText;
    [SerializeField] private bool updateEntrypoint = false;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (updateEntrypoint)
                StateManager.State.entryScene = targetScene;
            SceneChanger.Instance.ChangeScene(targetScene, transitionText);
        }
    }
}
