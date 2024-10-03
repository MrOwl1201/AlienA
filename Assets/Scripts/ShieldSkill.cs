using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShieldSkill : MonoBehaviour
{
    public GameObject shieldEffect;
    public float shieldDuration = 2f;
    public float cooldownTime = 5f;
    public Image cooldownImage;
    public float damageReduction = 50f;
    private bool isShieldActive = false;
    private bool isCooldown = false;
    public bool skill3Enabled;

    private void Start()
    {
        if(!skill3Enabled)
        {
            cooldownImage.fillAmount = 0f;
        }
        else
        {
            cooldownImage.fillAmount = 1f;
        }

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isCooldown&&skill3Enabled)
        {
            StartCoroutine(ActivateShield());
        }

        if (isCooldown)
        {
            cooldownImage.fillAmount -= 1 / cooldownTime * Time.deltaTime;
        }
    }

    IEnumerator ActivateShield()
    {
        AudioManager.instance.PlayShield();
        GameObject shield = Instantiate(shieldEffect, transform.position, Quaternion.identity, transform);
        isShieldActive = true;
        isCooldown = true;
        cooldownImage.fillAmount = 1;
        yield return new WaitForSeconds(shieldDuration);
        Destroy(shield);
        isShieldActive = false;
        yield return new WaitForSeconds(cooldownTime - shieldDuration);
        isCooldown = false;
        cooldownImage.fillAmount = 1f;
    }

    public float CalculateDamage(float incomingDamage)
    {
        float finalDamage = incomingDamage;
        if (isShieldActive)
        {
            finalDamage= Mathf.Max(0,incomingDamage -damageReduction);
        }
        return finalDamage;
    }
}

