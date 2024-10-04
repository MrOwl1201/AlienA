using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealthBar : MonoBehaviour
{
    private Vector3 initialScale;
    private EnemyAI enemy1; 
    private EnemyController enemy2; 
    private BossController enemy3; 

    void Start()
    {

        initialScale = transform.localScale;

        enemy1 = GetComponentInParent<EnemyAI>();
        enemy2 = GetComponentInParent<EnemyController>();
        enemy3 = GetComponentInParent<BossController>();
    }

    void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
        if (enemy1 != null && enemy1.movingRight)
        {
            transform.localScale = new Vector3(Mathf.Abs(initialScale.x), initialScale.y, initialScale.z);
        }
        else if (enemy2 != null && enemy2.movingRight)
        {
            transform.localScale = new Vector3(Mathf.Abs(initialScale.x), initialScale.y, initialScale.z);
        }
        else if (enemy3 != null && enemy3.movingRight)
        {
            transform.localScale = new Vector3(Mathf.Abs(initialScale.x), initialScale.y, initialScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(initialScale.x), initialScale.y, initialScale.z);
        }
    }
}

