using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public float damagePerSecond = 5f;
    private bool playerInZone = false;
    private GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
            playerInZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.GetComponent<PlayerController>().debuff.fillAmount = 0f;
            playerInZone = false;
        }
    }

    private void Update()
    {
        if (playerInZone)
        {
            player.GetComponent<PlayerController>().debuff.fillAmount = 1f;
             player.GetComponent<PlayerController>().TakeDamage(damagePerSecond * Time.deltaTime);
        }
    }
}

