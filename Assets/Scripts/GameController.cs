using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public void SaveGame()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("SavedScene", currentScene);
        PlayerPrefs.Save();
        Debug.Log("Game Saved: " + currentScene);
    }
    public void RestartLevel()
    {
        AudioManager.instance.PlaySoundEffect(15);
        Time.timeScale = 1;
        ScoreManager.instance.SaveScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Home()
    {
        AudioManager.instance.PlaySoundEffect(15);
        Time.timeScale = 1;
        SaveGame();
        ScoreManager.instance.SaveScore();
        SceneManager.LoadScene("MainMenu");
        
    }
    public void Tutorial()
    {
        AudioManager.instance.PlaySoundEffect(15);
        Time.timeScale = 1;
        SaveGame();
        ScoreManager.instance.SaveScore();
        SceneManager.LoadScene("Tutorial");

    }
    public void NextLevel()
    {
        AudioManager.instance.PlaySoundEffect(15);
        Time.timeScale = 1;
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        PlayerPrefs.SetInt("CurrentLevel", nextLevel);
        PlayerPrefs.Save();
        ScoreManager.instance.SaveScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
        SaveGame();
    }
}
