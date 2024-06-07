using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpawnLvl2 : MonoBehaviour
{
    public GameObject platformPrefab;
    public float spawnInterval = 2f;
    public float spawnDistance = 10f;
    public float destroyDelay = 2f;

    private Transform playerTransform;
    private float nextSpawnTime;
    private bool isSpawning = false; // Initially set to false

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (isSpawning && Time.time >= nextSpawnTime)
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
    }

    public void StartSpawning()
    {
        isSpawning = true;
        nextSpawnTime = Time.time + spawnInterval;
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }
}
