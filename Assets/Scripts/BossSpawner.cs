using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    public float spawnDistance = 10f;
    public float destroyDelay = 2f;
    public int maxSpawnCount = 1; 
    private Transform playerTransform;
    private float nextSpawnTime;
    private int spawnCount = 0;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        nextSpawnTime = Time.time + spawnInterval;
    }

    void FixedUpdate()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnBoss();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnBoss()
    {
        Vector3 spawnPosition = playerTransform.position + Vector3.forward * spawnDistance;
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        Destroy(newEnemy, destroyDelay);
        spawnCount++;
    }
}