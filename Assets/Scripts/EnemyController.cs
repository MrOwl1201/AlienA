using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    private float speed = 1f;
    private float chaseSpeed = 1.5f;
    private float patrolDistance = 8f; 
    private float detectionRange = 4f; 
    private float shootingRange = 5f;
    public GameObject bulletPrefab; 
    public Transform firePoint; 

    private Vector2 patrolStart;
    private Vector2 patrolEnd;
    public bool movingRight = true;
    private Transform player;
    private float lastShotTime;
    private bool isShooting = false;
    private float shootCooldown = 3f;
    private float nextShootTime = 0f;

    [Header("Health Settings")]
    public float maxHealth = 100f;
    private float currentHealth;
    public Image healthBarFill;
    public TextMeshProUGUI healthBarText;
    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        currentHealth = maxHealth;
        patrolStart = transform.position;
        patrolEnd = patrolStart + new Vector2(patrolDistance, 0);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        UpdateHealthBar();
    }

    void Update()
    {
        animator.SetFloat("health", currentHealth);
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {

            if ( distanceToPlayer <= shootingRange && Time.time >= nextShootTime && !isShooting)

            {
                animator.SetBool("IsIdle", true);
                FacePlayer();
                animator.SetBool("IsShoot", true);
            }
        }
        else
        {
            isShooting = false;
            animator.SetBool("IsIdle", false);
            Patrol();
        }
        animator.SetBool("IsRun", false);
        UpdateHealthBar();
        UpdateEnemyDirection();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Vector2 direction = (transform.position - other.transform.position).normalized;
            rb.velocity = direction * speed;
        }
    }
    void Patrol()
    {
        animator.SetBool("IsRun", true);
        if (movingRight)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolEnd, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolEnd) < 0.1f)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolStart, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolStart) < 0.1f)
            {
                movingRight = true;
            }
        }
    }

    void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
        if (player.position.x > transform.position.x)
        {
            movingRight = true;
        }
        else
        {
            movingRight = false;
        }
    }
    IEnumerator Shoots()
    {
       isShooting = true;

       yield return new WaitForSeconds(0.5f);
       animator.SetBool("IsShoot", false);
       Shoot();
       yield return new WaitForSeconds(shootCooldown);
        nextShootTime = Time.time + shootCooldown;
        isShooting = false;     
    }
    void Shoot()
    {
        AudioManager.instance.PlayEnemyShoot();
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogError("Bullet prefab hoặc fire point bị null.");
            return;
        }

        //Vector2 direction = (movingRight) ? Vector2.right : Vector2.left;
        Vector2 direction = (transform.localScale.x > 0) ? Vector2.right : Vector2.left;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        EnemyBullet bulletScript = bullet.GetComponent<EnemyBullet>();
        if (movingRight)
        {

            bulletScript.SetDirection(Vector2.right);
        }
        else
        {

            bulletScript.SetDirection(Vector2.left);
            Vector3 bulletScale = bullet.transform.localScale;
            bulletScale.x *= -1;
            bullet.transform.localScale = bulletScale;
        }
        
    }
    public void TakeDamage(float amount)
    {
        AudioManager.instance.PlayEnemyHurt();
        currentHealth -= amount;
        UpdateHealthBar();
        if (currentHealth < 0)
            currentHealth = 0;
        if (currentHealth > 0 && currentHealth < 100)
        {
            animator.SetTrigger("Hurt");
        }
        if (currentHealth <= 0)
        {
            animator.SetTrigger("Die");
            Die();
        }
        animator.SetBool("IsHurt", false);
    }

    void Die()
    {
         animator.SetTrigger("Dead");
        AudioManager.instance.PlayEnemyDie();
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        ScoreManager.instance.AddScore(800);
        Destroy(gameObject, 1f);
    }

    void UpdateHealthBar()
    {
        healthBarFill.fillAmount = (float)currentHealth / maxHealth;
        healthBarText.text=currentHealth.ToString();
    }
    void FacePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        if (player.position.x > transform.position.x)
        {
            movingRight = true;
        }
        else
        {
            movingRight = false;
        }
    }
    void UpdateEnemyDirection()
    {
        if (movingRight)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}

