using System.Collections;
using System.Collections.Generic;
using Bryndzaky.General.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneChanger.Instance.ChangeScene(StateManager.State.entryScene, StateManager.State.entryScene == "level_1" ? "Triezviem..." : "Loading...");
        // SceneManager.LoadScene();
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        // SceneManager.LoadScene("Hub");
    }

    public void Quit() 
    {
        StateManager.ManualSave(true);
        Application.Quit();
    }
}
