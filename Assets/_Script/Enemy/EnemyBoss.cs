using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyBoss : EnemyBase
{
    [SerializeField]
    GameObject Egg;
    private float eggCD = 1f;
    private Animator animator;
    private float controlCD = 2f;
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }
    protected override void OnEnable()
    {
        controlCD = 2f;
        GameManager.instance.ToggleMob();
        base.OnEnable();
        HP = 200f * GameManager.instance.EnemyHPScale;
        transform.DOMove(new Vector3(0, 2, 0), 2f);
    }
    void Update()
    {
        if(controlCD <= 0f && Time.timeScale > 0f)
        {
            Cursor.lockState = CursorLockMode.Confined;
            FollowCursor();
            Egging();
        }
        else
        {
            controlCD -= Time.deltaTime;
        }
    }
    void FollowCursor()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = pos;
    }
    public override void TakeHit(int BulletMod)
    {
        animator.SetTrigger("Hurt");
        switch (BulletMod)
        {
            case 0:
                HP -= 10f * GameManager.instance.PlayerDmgMod * 0.5f;
                break;
            case 1:
                HP -= 12f * GameManager.instance.PlayerDmgMod * 1.5f;
                break;
            default:
                break;
        }
        if (HP <= 0)
        {
            GameManager.instance.AddScore(2000);
        }
        base.TakeHit(BulletMod);
    }
    private void OnDisable()
    {
        if(GameManager.instance != null)
        {
            GameManager.instance.ToggleMob();
            GameManager.instance.CountDownBoss();
        }
        if(PowerUpPanel.instance != null)
        {
            Cursor.lockState = CursorLockMode.None;
            PowerUpPanel.instance.gameObject.SetActive(true);
        }
    }
    private void Egging()
    {
        if (eggCD <= 0f)
        {
            animator.SetTrigger("Attack");
            for (int i = -4; i <=4 ; ++i )
            {
                Vector3 spawnPos = transform.position;
                spawnPos.x += i;
                spawnPos.y -= 1f;
                Quaternion rotation = transform.rotation;
                Spawner.instance.SpawnObj(Egg, spawnPos, rotation);
                eggCD = 1f;
            }
            
        }
        eggCD -= Time.deltaTime;
    }
}
