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
    }
    public static ObjectPool _instance;
    [SerializeField] private Pool[] pools = null;
    // Start is called before the first frame update

    private void Awake()
    {
        _instance = this;
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i].pooledObjects = new Queue<GameObject>();
            for (int j = 0; j < pools[i].poolSize; j++)
            {
                GameObject obj = Instantiate(pools[i].objectPrefab);
                obj.SetActive(false);

                pools[i].pooledObjects.Enqueue(obj);
            }
        }
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
            GameObject obj = Instantiate(pools[objType].objectPrefab);
            obj.SetActive(false);
            pools[objType].pooledObjects.Enqueue(obj);
        }
    }
}
