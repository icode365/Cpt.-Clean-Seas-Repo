using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    #region ----Singleton----

    private static PoolManager instance;
    public static PoolManager Instance
    {
        get
        {
            if (instance == null)
                return instance;

            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    #endregion

    Dictionary<int, Queue<GameObject>> instantiatedObjects = new();
    
    public void InstatiatePoolPrefabs(GameObject prefab, int instantiateCount)
    {
        int prefabId = prefab.GetInstanceID();

        if (!instantiatedObjects.ContainsKey(prefabId))
        {
            Queue<GameObject> tempQueue = new();
            
            for(int i = 0; i < instantiateCount; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                tempQueue.Enqueue(obj);
            }
            instantiatedObjects.Add(prefabId, tempQueue);
        }

        if(instantiatedObjects.ContainsKey(prefabId))
        {
            for(int i = 0; i < instantiateCount; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                instantiatedObjects[prefabId].Enqueue(obj);
            }
        }
    }

    public GameObject GetGameObject(GameObject poolObject)
    {
        int prefabId = poolObject.GetInstanceID();

        if (!instantiatedObjects.ContainsKey(prefabId)) return null;

        foreach(GameObject obj in instantiatedObjects[prefabId])
        {
            if(!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return (obj);
            }    
        }

        InstatiatePoolPrefabs(poolObject, 5);
        return GetGameObject(poolObject);
    }
}
