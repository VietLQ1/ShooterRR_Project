using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField]
    GameObject deathFX;
    [SerializeField]
    AudioClip hitSFX;
    private int maxHP;
    private int curHP;
    private Rigidbody2D rb;
    private Collider2D col;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = rb.GetComponent<Collider2D>();
        maxHP = 10;
        curHP = 10;
    }
    private void Start()
    {
        UpdateHP();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            curHP -= 1;
            UpdateHP();
            //Debug.Log(curHP.ToString());
            Spawner.instance.SpawnObj(deathFX, transform.position, transform.rotation);
            AudioManager.instance.PlaySound(hitSFX);
        }
        if(curHP == 0)
        {
            UnlockCursor();
            GameManager.instance.EndingGame();
            Destroy(gameObject);
        }
    }
    public void FullyHeal()
    {
        curHP = maxHP;
        UpdateHP();
    }
    public void MaxHPUp(int hpincrease)
    {
        maxHP += hpincrease;
        curHP += hpincrease;
        UpdateHP();
    }
    private void UpdateHP()
    {
        GameManager.instance.SetHpBar(curHP, maxHP);
    }
    void UnlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
