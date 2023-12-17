using System.Collections;
using System.Collections.Generic;
using Bryndzaky.General.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(StateManager.State.entryScene);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        // SceneManager.LoadScene("Hub");
    }

    public void Quit() 
    {
        StateManager.ManualSave(true);
        Application.Quit();
    }
}
