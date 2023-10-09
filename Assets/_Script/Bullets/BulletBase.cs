using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BulletBase : MonoBehaviour
{
    public int BulletMod { get; protected set; }

    [SerializeField]
    protected GameObject shoot_effect;
    [SerializeField]
    protected GameObject hit_effect;

    protected Rigidbody2D rb;
    protected Collider2D coll;
    protected float timer;
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        
    }
    protected virtual void Start()
    {
        
    }
    protected virtual void OnEnable()
    {
        Spawner.instance.SpawnObj(shoot_effect, transform.position, Quaternion.identity);
        timer = 0f;
        
    }
    protected void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer > 2f)
        {
            Spawner.instance.Despawn(gameObject);
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("Hit");
            Spawner.instance.SpawnObj(hit_effect, transform.position, Quaternion.identity);
            Spawner.instance.Despawn(gameObject);
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlayParticleFX();
            }
            EnemyEgg enemyE = col.gameObject.GetComponent<EnemyEgg>();
            if (enemyE != null)
            {
                Spawner.instance.Despawn(enemyE.gameObject);
            }
            EnemyMob enemyM = col.gameObject.GetComponent<EnemyMob>();
            if(enemyM != null)
            {
                enemyM.TakeHit(BulletMod);
                return;
            }
            EnemyObstacle enemyO = col.gameObject.GetComponent<EnemyObstacle>();
            if(enemyO != null)
            {
                enemyO.TakeHit(BulletMod);
                return;
            }
            EnemyBoss enemyB = col.gameObject.GetComponent<EnemyBoss>();
            if(enemyB != null)
            {
                enemyB.TakeHit(BulletMod);
            }
        }
    }
}
