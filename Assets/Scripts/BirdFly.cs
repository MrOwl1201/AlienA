using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdFly : MonoBehaviour
{
    public float speed = 0.5f;
    public float maxDistance = 20;
    private Rigidbody2D rb2d;
    private float distance = 0;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        distance = 0;
    }
    void Update()
    {
        distance += Time.deltaTime * 2;
        if (distance > maxDistance)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            distance = 0;
        }
        rb2d.velocity = new Vector2(-transform.localScale.x, 0) * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            gameObject.SetActive(false);
            ScoreManager.instance.AddScore(100);
        }

    }
}
