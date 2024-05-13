using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickUp : MonoBehaviour
{
    public GameObject speedPrefab;
    public float spawnInterval = 2f;
    public float spawnDistance = 3f;
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
            SpawnSpeed();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnSpeed()
    {
        Vector3 spawnPosition = playerTransform.position + Vector3.forward * spawnDistance;
        GameObject newSpeed = Instantiate(speedPrefab, spawnPosition, Quaternion.identity);
        Destroy(newSpeed, destroyDelay);
    }
}
