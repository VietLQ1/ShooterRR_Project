using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private GameObject[] bullets;

    private float shootCD = 2.5f;
    private void PreLoadBullet()
    {
        for(int i = 0; i <100; i++)
        {
            Spawner.instance.SpawnObj(bullets[0].gameObject, new Vector3(99, 99, -10), Quaternion.identity);
            Spawner.instance.SpawnObj(bullets[1].gameObject, new Vector3(99, 99, -10), Quaternion.identity);
        }
    }
    void Start()
    {
        PreLoadBullet();
    }

    // Update is called once per frame
    void Update()
    {
        HandleGunShoot();
    }
    void HandleGunShoot()
    {
        if(shootCD > 0f)
        {
            shootCD -= Time.deltaTime;
            return;
        }
        Vector3 spawnPos = transform.position;
        Quaternion rotation = transform.rotation;

        if (Input.GetMouseButtonDown(0))
        {
            //shootHE
            Spawner.instance.SpawnObj(bullets[0], spawnPos, rotation);
            shootCD = 0.2f;
        }
        if (Input.GetMouseButtonDown(1))
        {
            //shootAP
            Spawner.instance.SpawnObj(bullets[1], spawnPos, rotation);
            shootCD = 0.2f;
        }
    }
}
