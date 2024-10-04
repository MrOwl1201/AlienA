using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text ScoreTextWin;
    public Text ScoreTextLose;
    public Text highScoreText;
    public int score = 0;
    private int highScore;
    private int currentLevelIndex;
    private string currentLevelName;

    public static ScoreManager instance;
    void Start()
    {
        currentLevelName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        highScore = PlayerPrefs.GetInt(currentLevelName + "_HighScore", 0);
        UpdateScoreUI();

    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
          //  DontDestroyOnLoad(gameObject);
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
    }
    public void AddScore(int amount)
    {
        score += amount;
       if(score >= highScore)
        {
            highScore = score;
        }    
            
        UpdateScoreUI();
    }
    public void SaveScore()
    {
        PlayerPrefs.SetInt(currentLevelName + "_HighScore", highScore);
        PlayerPrefs.Save();
    }
    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
        highScoreText.text = "High Score: " + highScore;
        ScoreTextWin.text = "Score: " + score;
        ScoreTextLose.text = "Score: " + score;
    }
}
