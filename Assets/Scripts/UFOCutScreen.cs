using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UFOCutScreen : MonoBehaviour
{
    public Transform ufo; 
    public Transform planet; 
    public float flySpeed = 1f; 
    private float minScale = 0.1f; 
    public float approachDistance = 0.5f; 

    private Vector3 ufoStartScale;
    private Vector3 ufoStartPosition;
    private bool isTransitioning = false;

    void Start()
    {
        ufoStartPosition = ufo.position;
        ufoStartScale = ufo.localScale;
    }

    void Update()
    {
        if (isTransitioning)
            return;
        ufo.position = Vector3.Lerp(ufo.position, planet.position, flySpeed * Time.deltaTime);
        float distance = Vector3.Distance(ufo.position, planet.position);
        float scaleValue = Mathf.Lerp(minScale, ufoStartScale.x, distance / approachDistance);
        ufo.localScale = new Vector3(scaleValue, scaleValue, 1);

        float epsilon = 0.01f; 
        if (distance <= approachDistance && Mathf.Abs(ufo.localScale.x - minScale) < epsilon)
        {
            Debug.Log("UFO đã đến hành tinh và thu nhỏ hoàn toàn!");
            StartCoroutine(TransitionToNextScene());
        }
        else
        {
          //  Debug.Log("Chưa đủ điều kiện để chuyển cảnh.");
        }

    }
    IEnumerator TransitionToNextScene()
    {
        isTransitioning = true;
        yield return new WaitForSeconds(1f);
       // Debug.Log("Đang chuyển cảnh.");
        SceneManager.LoadScene("Scene01");
    }
    public void Skip()
    {
        SceneManager.LoadScene("Scene01");
    }    
}


