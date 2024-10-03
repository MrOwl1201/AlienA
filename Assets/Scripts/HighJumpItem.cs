using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HighJumpItem : MonoBehaviour
{
    private float jumpBoost = 3f;
    private float boostDuration = 12f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                AudioManager.instance.PlayHighJump();
                player.StartCoroutine(player.HighJump(boostDuration, jumpBoost));
                
            }
            Destroy(gameObject);
        }
    }
}

