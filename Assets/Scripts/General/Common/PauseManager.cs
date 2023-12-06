using System.Collections;
using System.Collections.Generic;
using Bryndzaky.Units.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    //public GameObject game;
    public GameObject pause;
    public static bool IsPaused { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        IsPaused = false;        
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Menu"))
            TogglePause();
    }

    public void TogglePause()
    {
        Debug.Log("Paused: " + !IsPaused);
        Time.timeScale = IsPaused ? 1 : 0;

        //game.SetActive(isPaused);
        pause.SetActive(!IsPaused);

        IsPaused = !IsPaused;
    }

    public void MainMenu() {
        // SceneManager.LoadScene(0);
        // Destroy(Player.Instance);
        SceneManager.LoadScene("MainMenu");
    }
}
