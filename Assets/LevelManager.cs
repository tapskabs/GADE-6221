using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    public GameObject[] levelPlatforms; // Array of platforms for different levels
    public float levelTransitionTime = 30f; // Time after which to transition to the next level

    public FloorSpawn floorSpawnScript; // Reference to the FloorSpawn script
    public BossSpawner bossSpawnerScript; // Reference to the BossSpawner script

    private Transform playerTransform;
    private PlayerMovement playerMovement; // Reference to the PlayerMovement script

    private int currentLevelIndex = 0; // Index of the current level

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerMovement = playerTransform.GetComponent<PlayerMovement>();
        StartCoroutine(LevelTransition());
    }

    IEnumerator LevelTransition()
    {
        while (true)
        {
            yield return new WaitForSeconds(levelTransitionTime);
            TransitionToNextLevel();
        }
    }

    void TransitionToNextLevel()
    {
        ClearCurrentLevel();
        StopSpawners();
        currentLevelIndex = GetRandomLevelIndex();
        StartCoroutine(MovePlayerToLevel(levelPlatforms[currentLevelIndex]));
    }

    void ClearCurrentLevel()
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

    int GetRandomLevelIndex()
    {
        // Return a random level index, different from the current one
        int nextLevelIndex;
        do
        {
            nextLevelIndex = Random.Range(0, levelPlatforms.Length);
        } while (nextLevelIndex == currentLevelIndex);

        return nextLevelIndex;
    }

    IEnumerator MovePlayerToLevel(GameObject levelPlatform)
    {
        if (playerMovement != null)
        {
            playerMovement.enabled = false; // Disable player movement during transition
        }

        // Ensure the player is slightly above the platform to avoid collisions
        Vector3 newPosition = levelPlatform.transform.position + Vector3.up * 1.5f;
        playerTransform.position = newPosition;

        // Wait a short time to ensure the player is properly placed
        yield return new WaitForSeconds(0.5f);

        if (playerMovement != null)
        {
            playerMovement.enabled = true; // Re-enable player movement
        }

        // Additional logic for new level initialization can be added here
    }
}
