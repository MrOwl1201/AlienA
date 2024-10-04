using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused = false; 
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
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false); 
        Time.timeScale = 1f;   
        isPaused = false;  
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;    
        isPaused = true;     
    }
    public void QuitGame()
    {
        Application.Quit(); 
    }
    public void ExitPause()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseMenu.SetActive(false);
    }
}

