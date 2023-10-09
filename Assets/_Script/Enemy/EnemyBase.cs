using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    protected float HP = 0;
    protected float timer;
    protected bool isDespawning;
    protected Rigidbody2D rb;
    protected Collider2D col;
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }
    protected virtual void Start()
    {

    }
    protected virtual void OnEnable()
    {
        timer = 0f;
    }
    protected void FixedUpdate()
    {
        Despawning();
    }
    public virtual void TakeHit(int BulletMod)
    {
        if(HP <=0)
        {
            Spawner.instance.Despawn(gameObject);
        }
    }
    protected void Despawning()
    {
        if (isDespawning)
        {
            timer += Time.fixedDeltaTime;
            if (timer > 10f)
            {
                Spawner.instance.Despawn(gameObject);
            }
        }
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(HP<=100)
            {
                Spawner.instance.Despawn(gameObject);
            }
        }
    }
}
