using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItem : MonoBehaviour
{
    private int scoreAmount = 200;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioManager.instance.PlayCoin();
            ScoreManager.instance.AddScore(scoreAmount);

            Destroy(gameObject);
        }
    }
}
