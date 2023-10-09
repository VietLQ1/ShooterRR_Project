using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEgg : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;
    private float timer;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }
    private void OnEnable()
    {
        timer = 0;
        rb.velocity = new Vector2(0, -1) * 10f;
    }
    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer > 2f)
        {
            Spawner.instance.Despawn(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Spawner.instance.Despawn(gameObject);
        }
    }
}
