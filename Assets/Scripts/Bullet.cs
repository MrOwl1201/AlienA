using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 5f;
    public float shootDamage = 20f;
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
        EnemyController healthA = hitInfo.GetComponent<EnemyController>();
        EnemyAI healthE = hitInfo.GetComponent<EnemyAI>();
        BossController healthB = hitInfo.GetComponent<BossController>();
        if (hitInfo.CompareTag("Enemy") || hitInfo.CompareTag("Ground"))
        {
            if (healthA != null)
            {
                healthA.TakeDamage(shootDamage);
            }
            if(healthE != null)
            {
                healthE.TakeDamage(shootDamage);
            }
            if (healthB != null)
            {
                healthB.TakeDamage(shootDamage);
            }
            if (impactEffect != null)
            {
                GameObject effect = Instantiate(impactEffect, transform.position, transform.rotation);
                Destroy(effect, 1f);
            }
            Destroy(gameObject);
        }
    }
}
