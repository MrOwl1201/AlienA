using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBoostItem : MonoBehaviour
{
    public float damageBoostAmount = 2.0f;
    public float boostDuration = 5.0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.ActivateDamageBoost(damageBoostAmount, boostDuration);
            }
            Destroy(gameObject);
        }
    }
}
