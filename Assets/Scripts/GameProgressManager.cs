using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameProgressManager : MonoBehaviour
{
    public static GameProgressManager Instance;
    public int playerLevel;
    public int playerScore;
    public GameObject gameWin;
    public GameObject gameLose;
    public GameObject SettingMenu;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
           // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void RestartLevel()
    {
        AudioManager.instance.PlayScene();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void OnGameWin()
    {
        ScoreManager.instance.SaveScore();
        gameWin.SetActive(true);
    }
    public void OnGameLose()
    {
        gameLose.SetActive(true);
    }
    public void OpenSetting()
    {
        SettingMenu.SetActive(true);
    }
    public void ExitSetting()
    {
        SettingMenu.SetActive(false);
    }
}

