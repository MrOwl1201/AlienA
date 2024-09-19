using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItem : MonoBehaviour
{
    // Start is called before the first frame update
    private int scoreAmount = 200;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioManager.instance.PlaySoundEffect(9);
            ScoreManager.instance.AddScore(scoreAmount);

            Destroy(gameObject);
        }
    }
}
