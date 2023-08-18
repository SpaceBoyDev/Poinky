using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;

    /// <summary>
    /// Prefabs to be pooled
    /// </summary>
    [System.Serializable]
    public struct ObjectsToPool
    {
        public string prefabType;
        public GameObject prefab;
        public int amountToPool;
        [HideInInspector]
        public List<GameObject> pooledObject;
    }

    [SerializeField]
    private List<ObjectsToPool> objPool;

    public void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Gets all the necessary info
    /// - What objects to pool
    /// - How many objects to pool
    /// - Sets all objects Active(false)
    /// - Adds the objects to the list
    /// </summary>
    private void Start()
    {
        for (int i = 0; i < objPool.Count; i++)
        {
            for (int j = 0; j < objPool[i].amountToPool; j++)
            {
                GameObject obj = Instantiate(objPool[i].prefab);
                obj.SetActive(false);
                objPool[i].pooledObject.Add(obj);
            }
        }
    }

    /// <summary>
    /// Searchs for the game object to pool.
    /// If it finds the object, it returns de number in list of that object.
    /// If it does not find the object, it return -1 so GetPooledObject won't work
    /// </summary>
    public int SearchPool(GameObject prefab)
    {
        for (int i = 0; i < objPool.Count; i++)
        {
            if (objPool[i].prefab == prefab)
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// Gets the object requested and sets it active.
    /// It gets the number in list of the object from SearchPool and finds it.
    /// If the object found is not active, it returns the object requested.
    /// If the object found is active, it instantiates a new object, adds it to the pool and list, and returns it.
    /// </summary>
    public GameObject GetPooledObject(int numPool)
    {
        for (int i = 0; i < objPool[numPool].pooledObject.Count; i++)
        {
            if (objPool[numPool].pooledObject[i].activeInHierarchy == false)
            {
                return objPool[numPool].pooledObject[i];
            }
        }

        GameObject obj = Instantiate(objPool[numPool].prefab);
        obj.SetActive(false);
        objPool[numPool].pooledObject.Add(obj);
        return obj;

    }

    public void ResetPool()
    {
        for (int i = 0; i < objPool.Count; i++)
        {
            objPool[i].prefab.SetActive(false);
            objPool[i].prefab.transform.position = Vector3.zero;
        }
    }
}
