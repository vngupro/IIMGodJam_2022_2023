using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PauseManager : MonoBehaviour
{

    public static bool isPaused = false;

    public GameObject pauseMenuUI;

    Enemy enemy;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }

    void Paused()
    {
        PlayerMovement.instance.enabled = false;
        
        Shooting.instance.enabled = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void Resume()
    {
        PlayerMovement.instance.enabled = true;
        

        Shooting.instance.enabled = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;

    }

    public void Quit()
    {
        Application.Quit();
    }
}
