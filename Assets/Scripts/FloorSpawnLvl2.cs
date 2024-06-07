using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpawnLvl2 : MonoBehaviour
{
    public GameObject platformPrefab;
    public float spawnInterval = 2f;
    public float spawnDistance = 10f;
    public float destroyDelay = 2f;
    public int maxSpawnCount = 20;

    private Transform playerTransform;
    private float nextSpawnTime;
    private int spawnCount = 0;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        nextSpawnTime = Time.time + spawnInterval;
    }

    void Update()
    {
        if (spawnCount < maxSpawnCount && Time.time >= nextSpawnTime)
        {
            SpawnPlatform();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnPlatform()
    {
        Vector3 spawnPosition = playerTransform.position + Vector3.forward * spawnDistance;
        GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        Destroy(newPlatform, destroyDelay);
        spawnCount++;
    }

    public void ResetSpawner()
    {
        spawnCount = 0;
        nextSpawnTime = Time.time + spawnInterval;
    }

    public void StopSpawning()
    {
        enabled = false;
    }
}
