using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Spawner : SingletonMonobehavior<Spawner>
{
    [SerializeField] 
    public Transform Holder;
    public List<PooledObjInfo> prefabs = new List<PooledObjInfo>();

    public GameObject SpawnObj(GameObject obj,Vector3 spawnPos, Quaternion spawnRot)
    {
        PooledObjInfo ObjToSpawn = null;
        foreach(PooledObjInfo pbi in prefabs)
        {
            if(pbi.name == obj.name)
            {
                ObjToSpawn = pbi;
                break;
            }
        }
        if(ObjToSpawn == null)
        {
            ObjToSpawn = new PooledObjInfo() { name = obj.name };
            prefabs.Add(ObjToSpawn);
        }

        GameObject Spawnable = null;
        foreach(GameObject b in ObjToSpawn.InActiveObj)
        {
            if (b != null)
            {
                Spawnable = b;
                break;
            }
        }
        if (Spawnable == null)
        {
            Spawnable = Instantiate(obj, spawnPos, spawnRot);
            Spawnable.transform.SetParent(Holder);
            Spawnable.gameObject.name = Spawnable.name.Substring(0, Spawnable.name.Length - 7);
        }
        else
        {
            Spawnable.transform.position = spawnPos;
            Spawnable.transform.rotation = spawnRot;
            ObjToSpawn.InActiveObj.Remove(Spawnable);
            Spawnable.SetActive(true);
        }
        return Spawnable;
    }

    public void Despawn(GameObject obj)
    {
        PooledObjInfo ObjToDeSpawn = null;
        foreach (PooledObjInfo pbi in prefabs)
        {
            if (pbi.name == obj.name)
            {
                ObjToDeSpawn = pbi;
                break;
            }
        }
        if(ObjToDeSpawn == null)
        {

        }
        else
        {
            obj.SetActive(false);
            ObjToDeSpawn.InActiveObj.Add(obj);
        }
    }
    public class PooledObjInfo
    {
        public string name;
        public List<GameObject> InActiveObj = new List<GameObject>();
    }
}
