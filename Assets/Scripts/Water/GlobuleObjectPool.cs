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

    private GameObject GenerateNewInstance()
    {
        GameObject temp = Instantiate(objectToPool);
        temp.SetActive(false);
        pooledObjects.Add(temp);

        return temp;

    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            
            if (!pooledObjects[i].activeInHierarchy)
            {
                Debug.Log(i);
                pooledObjects[i].SetActive(true);
                return pooledObjects[i];
            }
        }
        GameObject newInstance = GenerateNewInstance();
        newInstance.SetActive(true);
        return newInstance;
    }


    public int GetObjectIndex(GameObject _object)
    {
        return pooledObjects.IndexOf(_object);
    }

}
