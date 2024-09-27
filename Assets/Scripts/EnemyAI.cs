using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    [Header("Movement Settings")]
    private float speed = 1f;
    private float chaseSpeed = 1.5f; 
    private float patrolDistance = 8f; 
    private float detectionRange = 5f;
    private Vector2 patrolStart;
    private Vector2 patrolEnd; 
    public bool movingRight = true;

    [Header("Attack Settings")]
    private float attackRange = 1f;
    private float damage = 20f;

    [Header("Health Settings")]
    private float maxHealth = 50f;
    private float currentHealth;
    public Image healthBarFill;
    public TextMeshProUGUI healthBarText;

    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isAttack = true;
    private float attackCooldown = 2f;
    private float nextAttackTime =0f;

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

        if (distanceToPlayer <= detectionRange&& distanceToPlayer > attackRange)
        {
            
           
               animator.SetBool("isIdle", false);
               ChasePlayer();
        }
        else if (distanceToPlayer <= attackRange && isAttack)
        {

            animator.SetBool("isIdle", true);
            StartCoroutine(Attack());
        }
        else
        {
            StopChasing();
        }
        UpdateHealthBar();
        UpdateEnemyDirection();
    }
    void StopChasing()
    {
        animator.SetBool("isRun", false);
        Patrol();
    }
    void Patrol()
    {

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

        animator.SetBool("isRun", true); 

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
    IEnumerator Attack()
    {
        isAttack = false;
        AudioManager.instance.PlaySoundEffect(11);
        animator.SetTrigger("Attack");
        float attackAnimationDuration = 1.0f;
        yield return new WaitForSeconds(attackAnimationDuration);
        Debug.Log("Attack animation triggered.");
        player.GetComponent<PlayerController>().TakeDamage(damage);
        yield return new WaitForSeconds(attackCooldown);
        nextAttackTime=Time.time+attackCooldown;
        isAttack = true;
        animator.SetBool("isAttack", false);
    }
    public void TakeDamage(float amount)
    {
        AudioManager.instance.PlaySoundEffect(13);
        currentHealth -= amount;
        UpdateHealthBar();
        if (currentHealth < 0)
            currentHealth = 0;
        if(currentHealth>0&&currentHealth<50)
        {
            animator.SetTrigger("Hurt");
        }
        if (currentHealth <= 0)
        {
            animator.SetTrigger("Die");
            Die();
        }
        animator.SetBool("isHurt", false);
    }

    void Die()
    {
        AudioManager.instance.PlaySoundEffect(14);
        ScoreManager.instance.AddScore(500);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        Destroy(gameObject, 1f);
        Debug.Log("EnemyG Died");
    }

    void UpdateHealthBar()
    {
        healthBarFill.fillAmount = (float)currentHealth / maxHealth;
        healthBarText.text = currentHealth.ToString();
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

