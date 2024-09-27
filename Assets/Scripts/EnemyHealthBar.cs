using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealthBar : MonoBehaviour
{
    private Vector3 initialScale;
    private EnemyAI enemy1;  // Thay đổi theo tên kẻ địch cụ thể
    private EnemyController enemy2;  // Thay đổi theo tên kẻ địch cụ thể
    private BossController enemy3;  // Thay đổi theo tên kẻ địch cụ thể

    void Start()
    {
        // Lưu lại giá trị scale ban đầu của Text
        initialScale = transform.localScale;

        // Lấy script Enemy từ đối tượng cha
        enemy1 = GetComponentInParent<EnemyAI>();
        enemy2 = GetComponentInParent<EnemyController>();
        enemy3 = GetComponentInParent<BossController>();
    }

    void LateUpdate()
    {
        // Cố định rotation của Text để không bị xoay
        transform.rotation = Quaternion.identity;

        // Kiểm tra hướng của kẻ địch
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
            // Nếu tất cả các kẻ địch đều không phải là phải
            transform.localScale = new Vector3(-Mathf.Abs(initialScale.x), initialScale.y, initialScale.z);
        }
    }
}

