using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private float explosionRadius = 1f;
    public LayerMask enemyLayer;
    public GameObject explosionEffect;
    public float bombDamage = 50f;

    void Update()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyLayer);
        if (enemies.Length > 0)
        {
            Explode();
        }
    }

    void Explode()
    {
        GameObject effect = Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(effect, 0.5f);
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyLayer);
        foreach (Collider2D enemy in enemies)
        {
            EnemyController enemyHealth = enemy.GetComponent<EnemyController>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(bombDamage);
            }
            EnemyAI enemyhealths = enemy.GetComponent<EnemyAI>();
            if (enemyhealths != null)
            {
                enemyhealths.TakeDamage(bombDamage);
            }
            BossController bossHealth = enemy.GetComponent<BossController>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(bombDamage);
            }
        }
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject,3f);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}

