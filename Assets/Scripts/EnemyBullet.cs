using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float speed = 5f;
    private int damage = 20;
    private Rigidbody2D rb;
    public GameObject impactEffect;
    private Vector2 direction;
    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed;
        Destroy(gameObject, 6f);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerController health = hitInfo.GetComponent<PlayerController>();
        if (hitInfo.CompareTag("Player") || hitInfo.CompareTag("Ground") )
        {
            if (health != null)
            {
                health.TakeDamage(damage);
            }
            if (impactEffect != null)
            {
                GameObject effect = Instantiate(impactEffect, transform.position, transform.rotation);
                Destroy(effect, 1f);
            }
            Destroy(gameObject);
        }    
            
    }
    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {

    //        Destroy(gameObject);
    //    }
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        PlayerController hurt = collision.gameObject.GetComponent<PlayerController>();
    //        hurt.TakeDamage(damage);
    //        Destroy(gameObject);
    //    }

    //}
}
