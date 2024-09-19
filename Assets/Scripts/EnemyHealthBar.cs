using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Image healthBar;
    private EnemyController enemy;

    void Start()
    {
        enemy = GetComponentInParent<EnemyController>();
    }

    void Update()
    {
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        //float healthRatio = (float)enemy.currentHealth / enemy.maxHealth;
        //healthBar.fillAmount = healthRatio;
    }
}
