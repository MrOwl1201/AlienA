using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public Text healthText;
    public void updateHealthBar(float currentHealth, float maxHealth)
    {
        healthText.text = currentHealth.ToString();
        healthBar.fillAmount = currentHealth/maxHealth;
    }
}