using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawn : MonoBehaviour
{
    public GameObject coinPrefab;
    public float spawnInterval = 2f;
    public float spawnDistance = 3f;
    public float destroyDelay = 10f;

    private Transform playerTransform;
    private float nextSpawnTime;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        nextSpawnTime = Time.time + spawnInterval;
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnCoin();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnCoin()
    {
        Vector3 spawnPosition = playerTransform.position + Vector3.forward * spawnDistance;
        GameObject newCoin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        Destroy(newCoin, destroyDelay);
    }
}
