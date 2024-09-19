using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerStart : MonoBehaviour
{
    public Transform player;
    public Transform dropPosition;
    public float dropSpeed = 5f; 
    public float flyAwaySpeed = 7f;

    private bool dropping = true;
    public QuestManager questManager;
    void Start()
    {
        player.position = transform.position;
    }

    void Update()
    {
        if (dropping)
        {
            player.position = Vector3.MoveTowards(player.position, dropPosition.position, dropSpeed * Time.deltaTime);

            if (Vector3.Distance(player.position, dropPosition.position) < 0.1f)
            {
                dropping = false;
                StartCoroutine(FlyAway());
            }
        }
    }

    IEnumerator FlyAway()
    {
        Vector3 flyAwayPosition = transform.position + Vector3.up * 10f;

        while (Vector3.Distance(transform.position, flyAwayPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, flyAwayPosition, flyAwaySpeed * Time.deltaTime);
            yield return null;
        }

        gameObject.SetActive(false);
        
    }
}

