using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] protected List<GameObject> pooledObjects;
    [SerializeField] protected GameObject objectToPool;
    [SerializeField] protected int amountToPool;

    protected virtual void Start()
    {
        InitializePool();
    }

    protected virtual void InitializePool()
    {
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < amountToPool; i++)
        {
            CreatePooledObject();
        }
    }

    protected virtual GameObject CreatePooledObject()
    {
        GameObject obj = Instantiate(objectToPool, transform);
        obj.SetActive(false);
        pooledObjects.Add(obj);
        return obj;
    }

    public virtual GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    public virtual void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
}
