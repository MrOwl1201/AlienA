using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    public int healAmount = 40;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                AudioManager.instance.PlayHeal();
                player.Heal(healAmount);
                Destroy(gameObject);
            }
        }
    }
}
