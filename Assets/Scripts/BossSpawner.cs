using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] GameObject bossPrefab;
    [SerializeField] float spawnInterval = 10f; // Time between boss spawns
    [SerializeField] float bossSpeed = 5f; // Boss movement speed
    [SerializeField] GameObject projectilePrefab; // Projectile to shoot
    [SerializeField] float spawnDistance = 20f; // Distance from the player to spawn the boss
    [SerializeField] float projectileLifetime = 5f; // Lifetime of projectiles

    float spawnTimer;
    Transform playerTransform;

    void Start()
    {
        spawnTimer = spawnInterval;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player not found. Make sure the player has the 'Player' tag assigned.");
        }
    }

    void Update()
    {
        // Count down the timer
        spawnTimer -= Time.deltaTime;

        // If the timer reaches zero or below, spawn the boss and reset the timer
        if (spawnTimer <= 0)
        {
            SpawnBoss();
            spawnTimer = spawnInterval;
        }
    }

    void SpawnBoss()
    {
        if (playerTransform == null)
        {
            Debug.LogError("Player transform not found.");
            return;
        }

        // Calculate spawn position at a certain distance from the player
        Vector3 spawnPosition = playerTransform.position + playerTransform.forward * spawnDistance;

        // Instantiate the boss prefab at the calculated position
        GameObject boss = Instantiate(bossPrefab, spawnPosition, Quaternion.identity);

        // Set boss movement speed
        boss.GetComponent<Rigidbody>().velocity = playerTransform.forward * bossSpeed;

        // Start boss shooting coroutine
        StartCoroutine(BossShoot(boss));
    }

    IEnumerator BossShoot(GameObject boss)
    {
        while (true)
        {
            if (playerTransform != null)
            {
                // Calculate direction from boss to player
                Vector3 directionToPlayer = (playerTransform.position - boss.transform.position).normalized;

                // Instantiate a projectile
                GameObject projectile = Instantiate(projectilePrefab, boss.transform.position, Quaternion.LookRotation(directionToPlayer));

                // Shoot the projectile towards the player
                projectile.GetComponent<Rigidbody>().velocity = directionToPlayer * 10f; // Adjust velocity as needed

                // Destroy the projectile after its lifetime
                Destroy(projectile, projectileLifetime);
            }

            yield return new WaitForSeconds(1f); // Adjust fire rate as needed
        }
    }
}