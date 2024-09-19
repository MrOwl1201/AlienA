using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFinish : MonoBehaviour
{
    public Transform player; 
    public Transform ufoPosition; 
    public float pullSpeed = 2f; 
    public float flyAwaySpeed = 4f;

    private bool pulling = false; 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            pulling = true;
        }
    }

    void Update()
    {
        if (pulling)
        {
            player.position = Vector3.MoveTowards(player.position, ufoPosition.position, pullSpeed * Time.deltaTime);

            if (Vector3.Distance(player.position, ufoPosition.position) < 0.1f)
            {
                StartCoroutine(FlyAwayWithPlayer());
            }
        }
    }

    IEnumerator FlyAwayWithPlayer()
    {
        Vector3 flyAwayPosition = transform.position + Vector3.up * 10f;

        while (Vector3.Distance(transform.position, flyAwayPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, flyAwayPosition, flyAwaySpeed * Time.deltaTime);
            player.position = transform.position;
            yield return null;
        }
        GameProgressManager.Instance.OnGameWin();
    }
}

