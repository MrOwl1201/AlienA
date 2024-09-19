using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public GameObject storyPanel; 
    public Text storyText; 
    public Text questText; 
    public Text skillText;
    public Button skipButton;  
    public float displayDuration = 10f; 

    private bool isSkipped = false;

    private void Start()
    {
        StartCoroutine(DisplayStoryAndQuest());
    }

    private IEnumerator DisplayStoryAndQuest()
    {
        storyPanel.SetActive(true);

        storyText.gameObject.SetActive(true);
        questText.gameObject.SetActive(true);
        skillText.gameObject.SetActive(true);

        skipButton.gameObject.SetActive(true);
        skipButton.onClick.AddListener(SkipStory);

        float elapsedTime = 0f;
        while (elapsedTime < displayDuration && !isSkipped)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        HideStory();
    }
    private void SkipStory()
    {
        isSkipped = true;
    }
    private void HideStory()
    {
        storyPanel.SetActive(false);
    }
    public void OpenStory()
    {
        storyPanel.SetActive(true);
    }
    public void ExitStory()
    {
        storyPanel.SetActive(false);
    }
}

