using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public LayerMask spawnLayer;
    
    public PoolObject[] _pools;
    public Vector3 startSpawnPos = new Vector3(0f, 6f, 0f);
    public float spawnInterval = 1f;
    public float spawnPosRange = 3.5f;
    
    void Start()
    {
        foreach (PoolObject pool in _pools)
            PoolManager.Instance.InstatiatePoolPrefabs(pool.prefab,10);

        
        StartCoroutine(StartSpawning());  
    }

    IEnumerator StartSpawning()
    {
        if (GameManager.gamePause) yield return null;

        Vector3 randomPos;
        int randomIndex;

        while (true)
        {
            randomIndex = Random.Range(0, _pools.Length);

            GameObject obj = PoolManager.Instance.GetGameObject
                (_pools[randomIndex].prefab);
            
            obj.SetActive(true);

            randomPos = new Vector3
                (Random.Range(-spawnPosRange, spawnPosRange), startSpawnPos.y, 0f);

            obj.transform.position = randomPos;

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
