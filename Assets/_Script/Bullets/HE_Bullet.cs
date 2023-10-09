using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HE_Bullet : BulletBase
{
    [SerializeField]
    private float bulletspeed = 10f;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        BulletMod = 0;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        rb.velocity = new Vector2(0, 1) * bulletspeed;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D (col);
    }
}
