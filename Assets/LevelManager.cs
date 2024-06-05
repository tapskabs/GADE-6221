using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject level2Platform; // The platform for Level 2
    public float levelTransitionTime = 30f; // Time after which to transition to Level 2

    public FloorSpawn floorSpawnScript; // Reference to the FloorSpawn script
    public BossSpawner bossSpawnerScript; // Reference to the BossSpawner script

    private Transform playerTransform;
    private PlayerMovement playerMovement; // Reference to the PlayerMovement script

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerMovement = playerTransform.GetComponent<PlayerMovement>();
        StartCoroutine(LevelTransition());
    }

    IEnumerator LevelTransition()
    {
        yield return new WaitForSeconds(levelTransitionTime);
        TransitionToLevel2();
    }

    void TransitionToLevel2()
    {
        ClearLevel1();
        StopSpawners();
        StartCoroutine(MovePlayerToLevel2());
    }

    void ClearLevel1()
    {
        // Destroy all objects with the "Level1" tag
        GameObject[] level1Objects = GameObject.FindGameObjectsWithTag("Level1");

        foreach (GameObject obj in level1Objects)
        {
            Destroy(obj);
        }
    }

    void StopSpawners()
    {
        // Disable the spawner scripts
        if (floorSpawnScript != null)
        {
            floorSpawnScript.enabled = false;
        }

        if (bossSpawnerScript != null)
        {
            bossSpawnerScript.enabled = false;
        }
    }

    IEnumerator MovePlayerToLevel2()
    {
        if (playerMovement != null)
        {
            playerMovement.enabled = false; // Disable player movement during transition
        }

        // Ensure the player is slightly above the platform to avoid collisions
        Vector3 newPosition = level2Platform.transform.position + Vector3.up * 1.5f;
        playerTransform.position = newPosition;

        // Wait a short time to ensure the player is properly placed
        yield return new WaitForSeconds(0.5f);

        if (playerMovement != null)
        {
            playerMovement.enabled = true; // Re-enable player movement
        }

        // Additional logic for Level 2 initialization can be added here
    }
}
