using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpawn : MonoBehaviour
{
    public GameObject platformPrefab;
    public float spawnInterval = 2f;
    public float spawnDistance = 10f;
    public float destroyDelay = 2f;

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
}
