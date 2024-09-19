using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    public float loseHeight = -100f;
    public float moveSpeed = 1f;
    public float jumpForce = 7f;

    public float fireCooldown = 2f;
    private float fireCooldownTimer;
    public Image cooldownImage;
    public KeyCode fireKey = KeyCode.K;
    public Image cooldownHighJump;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    public float maxHealth = 100f;
    public HealthBar healthBar;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public float attackDamage = 20f;
    public float attackRate = 2f;
    public Image debuff;
    private float originalShootDamage;
    private float originalBombDamage;
    private bool isBoostActive = false;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;
    private float currentHealth;
    private float nextFire = 0f;
    public bool isFacingRight = true;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public ShieldSkill shieldSkill; 
    public bool skill1Enabled;
    private AudioManager audioManager;
    private Bomb bombScript;
    private Bullet bulletScript;
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found!");
        }
        fireCooldownTimer = 0f;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.updateHealthBar(currentHealth, maxHealth);
        cooldownHighJump.fillAmount = 0f;
        debuff.fillAmount = 0f;
        originalShootDamage = bulletScript.shootDamage;
        originalBombDamage = bombScript.bombDamage;
    }

    void Update()
    {
        anim.SetFloat("Health", currentHealth);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        anim.SetBool("isGrounded", isGrounded);
        Move();
        Jump();
        
        if (fireCooldownTimer > 0)
        {
            fireCooldownTimer -= Time.deltaTime;
            cooldownImage.fillAmount = fireCooldownTimer / fireCooldown;
        }
        if (fireCooldownTimer <= 0f)
        {
            cooldownImage.fillAmount = 1f; // Biểu tượng kỹ năng đầy
        }
        // Kiểm tra nếu phím bắn súng được nhấn và không trong trạng thái cooldown
        if (Input.GetKeyDown(fireKey) && fireCooldownTimer <= 0&& skill1Enabled )
        {
            anim.SetTrigger("Shoot");
            StartCoroutine(ShootWithDelay());

            fireCooldownTimer = fireCooldown;
        }
        
        if (gameObject.transform.position.y < loseHeight)
        {
            gameObject.SetActive(false);

            GameProgressManager.Instance.OnGameLose();
            Time.timeScale = 0;
        }
        else
            Time.timeScale = 1;
        
        healthBar.updateHealthBar(currentHealth, maxHealth);
    }
    void Move()
    {
        AudioManager.instance.PlaySoundEffect(0);
        float moveInput = Input.GetAxis("Horizontal");
       // Debug.Log($"Move Input: {moveInput}");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (moveInput > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && isFacingRight)
        {
            Flip();
        }
        anim.SetFloat("Speed", Mathf.Abs(moveInput));
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void Jump()
    {
        AudioManager.instance.PlaySoundEffect(1);
        if (Input.GetButtonDown("Jump") && isGrounded )
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            anim.SetBool("IsJump", true);
        }
       

    }
    IEnumerator ShootWithDelay()
    {
        yield return new WaitForSeconds(2.5f);
       // Shoot();
        anim.SetBool("IsShoot", false);
    }
        
    void Shoot()
    {
        nextFire = Time.time + fireRate;
        AudioManager.instance.PlaySoundEffect(2);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (isFacingRight)
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
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetBool("IsJump", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void TakeDamage(float damage)
    {  
        damage = shieldSkill.CalculateDamage(damage);
        if(damage > 0)
        {
            AudioManager.instance.PlaySoundEffect(6);
            currentHealth -= damage;
            if(currentHealth < 0)
                currentHealth = 0;
            healthBar.updateHealthBar(currentHealth, maxHealth);
            if (currentHealth > 0 && currentHealth < 100)
            {
                anim.SetTrigger("Hurt");
            }

            if (currentHealth <= 0)
            {
                anim.SetTrigger("Dead");
                 Die();
            }
        }
        else
        {
            AudioManager.instance.PlaySoundEffect(4);
        }    
        
    }

    void Die()
    {
        AudioManager.instance.PlaySoundEffect(7);
        gameObject.SetActive(false);
        GameProgressManager.Instance.OnGameLose();
        Debug.Log("Player Died");
    }

    public IEnumerator HighJump(float duration, float boostAmount)
    {
        AudioManager.instance.PlaySoundEffect(8);
        cooldownHighJump.fillAmount = 1f; 
        jumpForce += boostAmount;
        float elapsedTime = 0f; 

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            cooldownHighJump.fillAmount = 1f - (elapsedTime / duration); 
            yield return null;
        }

        jumpForce -= boostAmount; 
        cooldownHighJump.fillAmount = 0f; 
   
    }
    public IEnumerator DamageUp(float duration, float boostAmount)
    {
        AudioManager.instance.PlaySoundEffect(8);
        cooldownHighJump.fillAmount = 1f;
        attackDamage += boostAmount;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            cooldownHighJump.fillAmount = 1f - (elapsedTime / duration);
            yield return null;
        }
        attackDamage -= boostAmount;
        cooldownHighJump.fillAmount = 0f;

    }
    public void Heal(int healAmount)
    {
        AudioManager.instance.PlaySoundEffect(5);
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.updateHealthBar(currentHealth, maxHealth);
    }
    public void ActivateDamageBoost(float boostAmount, float duration)
    {
        if (!isBoostActive)
        {
            StartCoroutine(DamageBoostCoroutine(boostAmount, duration));
        }
    }

    private IEnumerator DamageBoostCoroutine(float boostAmount, float duration)
    {
        isBoostActive = true;

        // Tăng sát thương của cả hai kỹ năng
        bulletScript.shootDamage += boostAmount;
        bombScript.bombDamage += boostAmount;

        // Chờ cho đến khi hết thời gian hiệu lực
        yield return new WaitForSeconds(duration);

        // Quay về sát thương ban đầu
        bulletScript.shootDamage = originalShootDamage;
        bombScript.bombDamage = originalBombDamage;

        isBoostActive = false;
    }

}