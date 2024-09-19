using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    [Header("Boss Attributes")]
    private float moveSpeed = 1f;
    private float detectionRange = 6f;
    private float chaseSpeed = 1.5f;
    private float enragedHealthThreshold = 0.3f;
    private float enragedDamageMultiplier = 1.5f;
    private float damage = 30f;
    public Image healthBar;
    public TextMeshProUGUI healthBarText;
    public GameObject enrageIcon;

    [Header("Skills")]
    private float skill1Cooldown = 4f;
    private float skill2Cooldown = 6f;
    private float skill1CooldownTimer = 0f;
    private float skill2CooldownTimer = 0f;
    public GameObject skill1Effect;
    public GameObject skill2Effect;
    public Transform groundCheck;
    public float groundRadius = 0.5f;
    public LayerMask playerLayer;
    public Image skill1Icon;
    public Image skill2Icon;

    private float maxHealth = 200f;
    private float currentHealth;
    public bool isEnraged = false;
    private bool isCooldown1 = true;
    private bool isCooldown2 = true;
    private Transform player;
    private Animator animator;
    private bool isMovingRight = true;
    private float attackRange = 2f;
    private Vector2 patrolStart; 
    private Vector2 patrolEnd; 
    public float patrolDistance = 8f;
                                      
    private float originalDamage;

    [Header("SkillsStomp")]
    private float stompDamage = 40f;
    public float stompRadius = 3f; 
    public float warningDuration = 2f; 
    public GameObject stompAreaIndicator;
    public Transform stompCenter; 
    private float originalStompDamage;

    private bool isAttacking = false;
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.fillAmount = 1;
        patrolStart = transform.position;
        patrolEnd = patrolStart + new Vector2(patrolDistance, 0);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        enrageIcon.SetActive(false);
        skill1Icon.fillAmount = 1f;
        skill2Icon.fillAmount = 1f;
        UpdateHealth();
        originalDamage = damage;
        originalStompDamage = stompDamage;
    }
    void Update()
    {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange && distanceToPlayer > attackRange)
        {
            
                ChasePlayer();          
        }
        else if (distanceToPlayer <= attackRange && (isCooldown1 || isCooldown2)&&!isAttacking)
        {
            StartCoroutine(StopAndAttack());
        }
        else
        {
            animator.SetBool("isRun", false);
            Patrol();
        }
        UpdateCooldowns();
        UpdateBossDirection();
        UpdateHealth();
    }
    void Patrol()
    {
        if (isMovingRight)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolEnd, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolEnd) < 0.1f)
            {
                isMovingRight = false;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolStart, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolStart) < 0.1f)
            {
                isMovingRight = true;
            }
        }
    }
    void ChasePlayer()
    {
        animator.SetBool("isRun", true);

        transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
        if (player.position.x > transform.position.x)
        {
            isMovingRight = true;
        }
        else
        {
            isMovingRight = false;
        }
    }
    IEnumerator StopAndAttack()
    {
        moveSpeed = 0f;
        chaseSpeed = 0f;
        int randomSkill = Random.Range(1, 3); 
        yield return new WaitForSeconds(1f);
        if(!isAttacking)
        {
            if (randomSkill == 1 && isCooldown1)
            {
                animator.SetTrigger("Attack"); 
            }
            else if (randomSkill == 2 && isCooldown2)
            {
                StartCoroutine(Stomp());
            }
        }     
        yield return new WaitForSeconds(2f);
        moveSpeed = 1f;
        chaseSpeed = 1.5f;
    }
    private IEnumerator UseSkill1()
    {
        isAttacking = true;
        isCooldown1 = false;
        skill1Icon.fillAmount = 0;
        AudioManager.instance.PlaySoundEffect(11);
        Debug.Log("Boss Attack!");
        if(player!=null)
        {
            player.GetComponent<PlayerController>().TakeDamage(damage);
            if (skill1Effect != null)
            {
                GameObject effect = Instantiate(skill1Effect, transform.position, transform.rotation);
                Destroy(effect, 1f);
            }
        }
        
        float cooldown = skill1Cooldown;
        while (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            skill1Icon.fillAmount = 1f - (cooldown / skill1Cooldown);
            yield return null;
        }
        isCooldown1 = true;
        skill1Icon.fillAmount = 1;
        isAttacking=false;
        animator.SetBool("isAttack", false);
    }
    IEnumerator Stomp()
    {
        isAttacking = true;
        isCooldown2 = false;
        skill2Icon.fillAmount = 0;
        stompAreaIndicator.SetActive(true); 
        stompAreaIndicator.transform.position = stompCenter.position;
        stompAreaIndicator.transform.localScale = new Vector3(stompRadius * 2, 0.2f, 1);
        yield return new WaitForSeconds(warningDuration);
        float cooldown = skill2Cooldown;
        while (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            skill2Icon.fillAmount = 1f - (cooldown / skill2Cooldown);
            yield return null;
        }
        stompAreaIndicator.SetActive(false);
        animator.SetTrigger("Skill2");
        Debug.Log("Boss dậm nhảy!");
        AudioManager.instance.PlaySoundEffect(15);
        DealStompDamage();
        yield return new WaitForSeconds(skill2Cooldown);
        animator.SetBool("isJump", false);
        skill2Icon.fillAmount = 1f;
        isCooldown2 = true;
        isAttacking = false;
    }
    void DealStompDamage()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(stompCenter.position, stompRadius);
        foreach (Collider2D hit in hitPlayers)
        {
            if (hit.CompareTag("Player"))
            {

                hit.GetComponent<PlayerController>().TakeDamage(stompDamage);
                Debug.Log("Gây sát thương cho: " + hit.name);
            }
            if (skill2Effect != null)
            {
                GameObject effect = Instantiate(skill2Effect, transform.position, transform.rotation);
                Destroy(effect, 1f);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(stompCenter.position, stompRadius);
    }

void UpdateHealth()
    {
        healthBar.fillAmount = (float)currentHealth / maxHealth;
        healthBarText.text=currentHealth.ToString();
        if (currentHealth <= maxHealth * enragedHealthThreshold && !isEnraged)
        {
            StartCoroutine(Enrage());
        }
    }
    private void UpdateCooldowns()
    {
        if (!isCooldown1)
        {
            skill1CooldownTimer += Time.deltaTime;
            skill1Icon.fillAmount = skill1CooldownTimer / skill1Cooldown;

            if (skill1CooldownTimer >= skill1Cooldown)
            {
                skill1CooldownTimer = 0f;
                isCooldown1 = true;
            }
        }
        if (!isCooldown2)
        {
            skill2CooldownTimer += Time.deltaTime;
            skill2Icon.fillAmount = skill2CooldownTimer / skill2Cooldown;

            if (skill2CooldownTimer >= skill2Cooldown)
            {
                skill2CooldownTimer = 0f;
                isCooldown2 = true;
            }
        }
    }
    void UpdateBossDirection()
    {

        if (isMovingRight)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
    IEnumerator Enrage()
    {
        AudioManager.instance.PlaySoundEffect(10);
        isEnraged = true;
        enrageIcon.SetActive(true);
        damage *= enragedDamageMultiplier;
        stompDamage *= enragedDamageMultiplier;
        yield return new WaitForSeconds(10f);
        damage = originalDamage;
        stompDamage = originalStompDamage;
        enrageIcon.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        AudioManager.instance.PlaySoundEffect(13);
        currentHealth -= damage;
        UpdateHealth();
        if(currentHealth < 0)
            currentHealth = 0;
        if(currentHealth > 0&&currentHealth<200)
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
        ScoreManager.instance.AddScore(1200);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        Destroy(gameObject, 1f); 
    }
}

