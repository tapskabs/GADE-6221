using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpawn : MonoBehaviour
{
    public GameObject platformPrefab;
    public float spawnInterval = 2f;
    public float spawnDistance = 10f;
    public float destroyDelay = 2f;
    public int maxSpawnCount = 10; // Maximum number of platforms to spawn in one level

    private Transform playerTransform;
    private float nextSpawnTime;
    private int spawnedPlatformsCount = 0; // Counter to keep track of spawned platforms
    private bool spawningEnabled = true; // Flag to control spawning
    private LevelManager levelManager;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        levelManager = FindObjectOfType<LevelManager>();
        nextSpawnTime = Time.time + spawnInterval;
    }

    void Update()
    {
        if (spawningEnabled && spawnedPlatformsCount < maxSpawnCount && Time.time >= nextSpawnTime)
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
        spawnedPlatformsCount++; // Increment the spawned platforms count

        if (spawnedPlatformsCount >= maxSpawnCount)
        {
            spawningEnabled = false; // Disable spawning when max count is reached
            levelManager.OnLevelCompleted(); // Notify the level manager
        }
    }

    public void ResetSpawner()
    {
        spawnedPlatformsCount = 0;
        spawningEnabled = true;
        nextSpawnTime = Time.time + spawnInterval;
    }
}