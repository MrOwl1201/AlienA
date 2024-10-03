using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuController : MonoBehaviour
{
    public Button continueButton;

    private void Start()
    {
        if (PlayerPrefs.HasKey("SavedScene"))
        {
            continueButton.gameObject.SetActive(true);
            continueButton.onClick.AddListener(LoadGame);
        }
        else
        {
            continueButton.gameObject.SetActive(false);
        }
    }
    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("SavedScene"))
        {
            string savedScene = PlayerPrefs.GetString("SavedScene");
            SceneManager.LoadScene(savedScene);
            Debug.Log("Loaded Game: " + savedScene);
        }
        else
        {
            Debug.Log("No saved game found!");
            SceneManager.LoadScene("CutScreenNew");
        }
    }
    public void StartGame()
    {
        ResetGame();
        SceneManager.LoadScene("CutScreenNew"); 
    }
    public void ResetGame()
    {
        PlayerPrefs.DeleteKey("SavedScene");
        PlayerPrefs.Save();
        Debug.Log("Game reset!");
    }
    public void SaveGame()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("SavedScene", currentScene);
        PlayerPrefs.Save();
        Debug.Log("Game Saved: " + currentScene);
    }
    public void OpenOptions()
    {
        AudioManager.instance.PlayScene();
        SceneManager.LoadScene("SettingScene");
    }
    public void Continue()
    {
        LoadGame() ;
    }
    public void Tutorial()
    {
        AudioManager.instance.PlayScene();
        SceneManager.LoadScene("Tutorial");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}

