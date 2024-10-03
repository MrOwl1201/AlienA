using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGate : MonoBehaviour
{
    public Transform teleportTarget;
    public GameObject player;
    private bool isTeleported = false; 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player && !isTeleported)
        {
            AudioManager.instance.PlayTeleport();
            player.transform.position = teleportTarget.position;
            StartCoroutine(TeleportCooldown());
        }
    }

    IEnumerator TeleportCooldown()
    {
        isTeleported = true;
        yield return new WaitForSeconds(1f);
        isTeleported = false;
    }
}
