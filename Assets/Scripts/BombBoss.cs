using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBoss : MonoBehaviour
{
    public float explosionDelay = 2f; 
    public float explosionRadius = 3f; 
    public float explosionForce = 40f; 
    public LayerMask playerLayer;
    public float bombDamage = 40f;
    public GameObject explosionEffect; 

    void Start()
    {    
        Invoke("Explode", explosionDelay);
    }

    void Explode()
    {
        GameObject effect= Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, playerLayer);

        foreach (Collider2D nearbyObject in colliders)
        {
            Rigidbody2D rb = nearbyObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.GetComponent<PlayerController>().TakeDamage(bombDamage);
                Vector2 explosionDir = nearbyObject.transform.position - transform.position;
                rb.AddForce(explosionDir.normalized * explosionForce);
            }
        }
        Destroy(gameObject);     
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
