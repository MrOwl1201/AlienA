using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarEnemy : MonoBehaviour
{
    public Transform enemy;

    void Update()
    {
        transform.position = enemy.position + new Vector3(0, 1f, 0);
    }
}

