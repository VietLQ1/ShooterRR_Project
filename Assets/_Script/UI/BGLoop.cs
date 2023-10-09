using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGLoop : MonoBehaviour
{
    [Range(-1f, 1f)]
    public float loopspeed = 0.2f;
    private MeshRenderer mesh;
    private float offset;
    // Start is called before the first frame update
    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Scroll();
    }
    void Scroll()
    {
        offset = loopspeed * Time.time;
        Vector2 scroll = new Vector2(0, offset);
        mesh.sharedMaterial.SetTextureOffset("_MainTex",scroll);
    }
}
