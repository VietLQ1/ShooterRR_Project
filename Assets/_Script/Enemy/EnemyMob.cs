using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMob : EnemyBase
{
    [SerializeField]
    GameObject Egg;
    [SerializeField]
    private float flyingdir = 1f;
    [SerializeField]
    AudioClip ChickenClip;
    private float eggCD = 1f;
    protected override void OnEnable()
    {
        base.OnEnable();
        HP = 10f * GameManager.instance.EnemyHPScale;
        rb.velocity = new Vector2(1,0) * 5f * flyingdir;
    }

    protected override void Awake()
    {
        base.Awake();
        isDespawning = true;
    }
    void Update()
    {
        Egging();
    }
    public override void TakeHit(int BulletMod)
    {
        switch(BulletMod)
        {
            case 0:
                HP -= 10f * GameManager.instance.PlayerDmgMod * 1.2f;
                break;
            case 1:
                HP -= 12f * GameManager.instance.PlayerDmgMod * 0.8f;
                break;
            default:
                break;
        }
        if (HP <= 0)
        {
            GameManager.instance.AddScore(10);
        }
        if(AudioManager.instance != null)
        {
            AudioManager.instance.PlaySound(ChickenClip);
        }
        base.TakeHit(BulletMod);
    }
    private void Egging()
    {
        if(eggCD <= 0f)
        {
            Vector3 spawnPos = transform.position;
            spawnPos.y -= 1f;
            Quaternion rotation = transform.rotation;
            Spawner.instance.SpawnObj(Egg, spawnPos, rotation);
            eggCD = 1.5f;
        }
        eggCD -= Time.deltaTime;
    }
}
