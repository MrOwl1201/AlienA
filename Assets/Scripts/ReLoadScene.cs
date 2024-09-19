using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReLoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    public void RestartLevel()
    {
        // T?i l?i màn ch?i hi?n t?i
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
