using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPickUp : MonoBehaviour
{
    public GameObject jumpPrefab;
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
            SpawnJump();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnJump()
    {
        Vector3 spawnPosition = playerTransform.position + Vector3.forward * spawnDistance;
        GameObject newJump = Instantiate(jumpPrefab, spawnPosition, Quaternion.identity);
        Destroy(newJump, destroyDelay);
    }
}
