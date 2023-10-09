using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObstacle : EnemyBase
{
    [SerializeField]
    AudioClip DestoyClip;
    [SerializeField]
    float spd = 8f;
    protected override void OnEnable()
    {
        base.OnEnable();
        HP = 5f * GameManager.instance.EnemyHPScale;
        //Debug.Log(HP.ToString());
        rb.velocity = new Vector2(0, -1) * spd;
    }
    protected override void Awake()
    {
        base.Awake();
        isDespawning = true;
        
    }
    void Update()
    {

    }
    public override void TakeHit(int BulletMod)
    {
        switch (BulletMod)
        {
            case 0:
                HP -= 10f * GameManager.instance.PlayerDmgMod;
                break;
            case 1:
                HP -= 12f * GameManager.instance.PlayerDmgMod;
                break;
            default:
                break;
        }
        if(HP <= 0)
        {
            GameManager.instance.AddScore(5);
        }
        if(AudioManager.instance != null)
        {
            AudioManager.instance.PlaySound(DestoyClip);
        }
        base.TakeHit(BulletMod);
    }
}
