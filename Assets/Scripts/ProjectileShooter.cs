using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public float shootInterval = 1f;

    private Transform playerTransform;
    private float nextShootTime;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        nextShootTime = Time.time + shootInterval;
    }

    void Update()
    {
        if (Time.time >= nextShootTime)
        {
            ShootProjectile();
            nextShootTime = Time.time + shootInterval;
        }
    }

    void ShootProjectile()
    {
        if (playerTransform == null)
        {
            Debug.LogError("Player transform not found.");
            return;
        }

        // Calculate direction from shooter to player
        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;

        // Instantiate a projectile
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.LookRotation(directionToPlayer));

        // Shoot the projectile towards the player
        projectile.GetComponent<Rigidbody>().velocity = directionToPlayer * projectileSpeed;
    }
}