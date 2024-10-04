using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject gate;

    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        gate.SetActive(false);
    }

    void Update()
    {
        CheckEnemies();
    }

    void CheckEnemies()
    {
        if (AreAllEnemiesDefeated())
        {
            gate.SetActive(true);
        }
    }

    bool AreAllEnemiesDefeated()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                return false;
            }
        }
        return true;
    }
}

