using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Pool {

    public string name;
    public GameObject prefab;
    public int size;
}

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler _instance;
    public List<Pool> pools;
    Dictionary<string, Queue<GameObject>> poolDictionary;

    public static ObjectPooler Instance
    {
        get { 
            if (_instance != null)
                _instance = new ObjectPooler();
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i <= pool.size - 1; i++)
            {
                GameObject obj = Instantiate(pool.prefab);

                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.name, objectPool);
        }
    }

    public void ReturnToPool( string tag, GameObject gObject)
    {
        poolDictionary[tag].Enqueue(gObject);
       // gObject.SetActive(false);
        
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            return null;
        }

        // take the first object
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        // poolDictionary[tag].Enqueue(objectToSpawn);
        ReturnToPool(tag, objectToSpawn);
        return objectToSpawn;
    }
}
