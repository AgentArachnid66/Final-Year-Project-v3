using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobuleObjectPool : MonoBehaviour
{
    public static GlobuleObjectPool sharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    public DataController dataController;

    private void Awake()
    {
        sharedInstance = this;
    }

    private void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject temp;
        for (int i = 0; i < amountToPool; i++)
        {
            temp = Instantiate(objectToPool);
            
            temp.SetActive(false);
            pooledObjects.Add(temp);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            
            if (!pooledObjects[i].activeInHierarchy)
            {
                Debug.Log(i);
                return pooledObjects[i];
            }
        }
        return null;
    }


    public int GetObjectIndex(GameObject _object)
    {
        return pooledObjects.IndexOf(_object);
    }

}
