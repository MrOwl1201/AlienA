using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credit : MonoBehaviour
{
    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void NextCredit()
    {
        SceneManager.LoadScene("Credit");
    }
}
