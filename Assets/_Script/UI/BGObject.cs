using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BGObject : MonoBehaviour
{
    private float timer = 0f;
    private void OnEnable()
    {
        timer = 0f;
    }
    private void Update()
    {
        transform.Translate(new Vector3(0,-1,0)*Time.deltaTime);
        if (timer > 50f)
        {
            Spawner.instance.Despawn(gameObject);
        }
        timer += Time.deltaTime;
    }
}
