using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpawn2 : MonoBehaviour
{
    public GameObject platformPrefab;
    public float spawnInterval = 2f;
    public float spawnDistance = 10f;
    public float destroyDelay = 2f;

    private Transform playerTransform;
    private float nextSpawnTime;
    private bool canSpawn = false; // Flag to check if spawning is allowed

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(DelayedStart());
    }

    void Update()
    {
        if (canSpawn && Time.time >= nextSpawnTime)
        {
            SpawnPlatform();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    IEnumerator DelayedStart()
    {
        // Wait for 20 seconds before allowing spawning to start
        yield return new WaitForSeconds(20f);
        canSpawn = true;
        nextSpawnTime = Time.time + spawnInterval;
    }

    void SpawnPlatform()
    {
        Vector3 spawnPosition = playerTransform.position + Vector3.forward * spawnDistance;
        GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        Destroy(newPlatform, destroyDelay);
    }
}