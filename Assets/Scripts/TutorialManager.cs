using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] tutorialSteps;
    private int currentStepIndex = 0;

    void Start()
    {
        ShowCurrentStep();
    }

    public void NextStep()
    {
        if (currentStepIndex < tutorialSteps.Length - 1)
        {
            tutorialSteps[currentStepIndex].SetActive(false);
            currentStepIndex++;
            ShowCurrentStep();
        }
    }

    public void PreviousStep()
    {
        if (currentStepIndex > 0)
        {
            tutorialSteps[currentStepIndex].SetActive(false);
            currentStepIndex--;
            ShowCurrentStep();
        }
    }

    private void ShowCurrentStep()
    {
        tutorialSteps[currentStepIndex].SetActive(true);
    }
   
    public void Home()
    {
        AudioManager.instance.PlayScene();
        SceneManager.LoadScene("MainMenu");
    }
    void StartGame()
    {
        AudioManager.instance.PlayScene();
        SceneManager.LoadScene("Scene01");
    }
}

