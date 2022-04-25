using System.Collections;
using UnityEngine;

public class CollectiblesSpawner : MonoBehaviour
{
    public PoolObject[] _pools;
    public Vector3 startSpawnPos;
    public float spawnInterval = 1f;
    public float spawnPosRange = 3.5f;

    private void Start()
    {
        foreach (PoolObject pool in _pools)
            PoolManager.Instance.InstatiatePoolPrefabs(pool.prefab, 10);
        
        StartCoroutine(SpawnCollectibles());
    }

    IEnumerator SpawnCollectibles()
    {
        if (GameManager.gamePause) yield return null;

        int randomIndex;
        float randomXPos;
        Vector3 spawnPos = startSpawnPos;
        GameObject obj;

        while (true)
        {
            randomIndex = Random.Range(0, _pools.Length);
            randomXPos = Random.Range(-spawnPosRange, spawnPosRange);

            obj = PoolManager.Instance.GetGameObject(_pools[randomIndex].prefab);

            obj.transform.position = spawnPos;
            obj.transform.parent = transform;

            spawnPos = new Vector3(randomXPos, spawnPos.y, 0f);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
