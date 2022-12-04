using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    Rigidbody2D rb;
    playermovement player;
    float xSpeed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<playermovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }

    void Update()
    {
        rb.velocity = new Vector2(xSpeed, 0.2f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.tag == "enemy")
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);  
    }
}
