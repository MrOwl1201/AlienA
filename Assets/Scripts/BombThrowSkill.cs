using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombThrowSkill : MonoBehaviour
{
    public int lineResolution = 10;
    public GameObject bombPrefab; 
    public Transform throwPoint; 
    public float minThrowForce = 5f; 
    public float maxThrowForce = 20f; 
    public float chargeTime = 1f; 
    public LineRenderer lineRenderer; 
    public Image cooldownIcon; 
    public float cooldownTime = 5f; 

    private bool isCharging = false;
    private float currentThrowForce;
    private bool isCooldown = false;
    public bool skill2Enabled;
    private PlayerController playerController;
    private Animator anim;
    void Start()
    {
        if (!skill2Enabled)
        {
            cooldownIcon.fillAmount = 0f;
        }
        playerController =GetComponent<PlayerController>();
        lineRenderer.positionCount = lineResolution;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        anim = GetComponent<Animator>();
           
        
        lineRenderer.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isCooldown&&skill2Enabled)
        {
            anim.SetBool("IsThrowBomb", true);
            isCharging = true;
            currentThrowForce = minThrowForce;
            StartCoroutine(ChargeThrowForce());
        }

        if (Input.GetKeyUp(KeyCode.E) && isCharging&&skill2Enabled)
        {
            anim.SetBool("IsThrowBomb", false);
            ThrowBomb();
            isCharging = false;
            lineRenderer.enabled = false;
        }

        if (isCharging)
        {
            UpdateTrajectory();
        }
    }
    IEnumerator ChargeThrowForce()
    {
        float chargeRate = (maxThrowForce - minThrowForce) / chargeTime;

        while (isCharging)
        {
            if (currentThrowForce < maxThrowForce)
            {
                currentThrowForce += chargeRate * Time.deltaTime;
            }
            yield return null;
        }
    }
    void UpdateTrajectory()
    {
        lineRenderer.enabled = true;

        float direction = playerController.isFacingRight ? 1f : -1f;

        float angleInRadians = 25f * Mathf.Deg2Rad;
        Vector2 throwDirection = new Vector2(Mathf.Cos(angleInRadians) * direction, Mathf.Sin(angleInRadians));


        Vector2 startPoint = throwPoint.position;
        Vector2 velocity = throwDirection * currentThrowForce;

        int numPoints = 30;
        float timeStep = 0.1f;

        lineRenderer.positionCount = numPoints;
        for (int i = 0; i < numPoints; i++)
        {
            float t = i * timeStep;
            Vector2 position = startPoint + velocity * t + 0.5f * Physics2D.gravity * t * t;
            lineRenderer.SetPosition(i, position);
        }
    }

    void ThrowBomb()
    {
        StartCoroutine(Cooldown());
        AudioManager.instance.PlayBomUp();
        GameObject bomb = Instantiate(bombPrefab, throwPoint.position, Quaternion.identity);

        float direction = playerController.isFacingRight ? 1f : -1f;
        float angleInRadians = 25f * Mathf.Deg2Rad;
        Vector2 throwDirection = new Vector2(Mathf.Cos(angleInRadians) * direction, Mathf.Sin(angleInRadians));
        Rigidbody2D bombRb = bomb.GetComponent<Rigidbody2D>();
        bombRb.AddForce(throwDirection * currentThrowForce, ForceMode2D.Impulse);
    }

    IEnumerator Cooldown()
    {
        isCooldown = true;
        cooldownIcon.fillAmount = 1;

        float cooldownRate = 1f / cooldownTime;
        while (cooldownIcon.fillAmount > 0)
        {
            cooldownIcon.fillAmount -= cooldownRate * Time.deltaTime;
            yield return null;
        }
        cooldownIcon.fillAmount = 1f;
        isCooldown = false;
    }       
}
