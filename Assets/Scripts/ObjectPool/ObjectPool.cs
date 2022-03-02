using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Serializable]
    public struct Pool
    {
        public Queue<GameObject> pooledObjects;
        public GameObject objectPrefab;
        public int poolSize;
        public int activeObjectCount;
        public Vector3 objectPos;
        public float posZ,posY,posX;
        public float randXMin, randXMax;
        
        public float incDist;
    }
    public static ObjectPool _instance;

    [SerializeField] Transform objectPoolParent;
    [SerializeField] private Pool[] pools = null;
    // Start is called before the first frame update


    public Vector3 GetPos(int type)
    {
        pools[type].objectPos = new Vector3(pools[type].posX, pools[type].posY, pools[type].posZ);
        return pools[type].objectPos;
    }

    public void GetRandomX(int type)
    {
       
       float rand = UnityEngine.Random.RandomRange(pools[type].randXMin, pools[type].randXMax);
       pools[type].posX = rand;

    }
     
    
    public void IncreasePosZ(int type)
    {
        pools[type].posZ += pools[type].incDist;
        
    }
    private void Awake()
    {
        _instance = this;
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i].pooledObjects = new Queue<GameObject>();
            for (int j = 0; j < pools[i].poolSize; j++)
            {
                GameObject obj = Instantiate(pools[i].objectPrefab, objectPoolParent.transform);
                obj.SetActive(false);

                pools[i].pooledObjects.Enqueue(obj);
            }
        }
    }
    public int GetActiveObjCount(int objType)
    {
        return pools[objType].activeObjectCount;
    }
    public int GetPoolTypeCount()
    {
        return pools.Length;
    }
    public int GetPoolSize(int poolType)
    {
        return pools[poolType].poolSize;
    }
    public GameObject GetPooledObject(int objType)
    {
        if (objType>=pools.Length)
        {
            return null;
        }
     
     
        GameObject obj = pools[objType].pooledObjects.Dequeue();
        obj.SetActive(true);

        pools[objType].pooledObjects.Enqueue(obj);
        return obj;
    }
    
    public void AddSizePool(int amount,int objType)
    {   
        for (int i = 0; i <amount; i++)
        {
            GameObject obj = Instantiate(pools[objType].objectPrefab, objectPoolParent.transform);
            obj.SetActive(false);
            pools[objType].pooledObjects.Enqueue(obj);
        }
    }
}
