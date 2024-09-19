using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int playerScore;
    public Vector3 playerPosition;
    private int currentLevelIndex;
}

public class SaveManager : MonoBehaviour
{
    public PlayerController player;
    public ScoreManager scoreManager;
    public static SaveManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
          //  DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        LoadGame();
    }

    public void SaveGame()
    {
        GameData data = new GameData
        {
            playerScore = scoreManager.score,
            playerPosition = player.transform.position
        };

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("SaveData", json);
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("SaveData"))
        {
            string json = PlayerPrefs.GetString("SaveData");
            GameData data = JsonUtility.FromJson<GameData>(json);

            scoreManager.score = data.playerScore;
            player.transform.position = data.playerPosition;
        }
    }
}

