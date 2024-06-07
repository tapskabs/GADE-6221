using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] levelPlatforms; // Array of platforms for different levels
    public float levelTransitionTime = 30f; // Time after which to transition to the next level

    public FloorSpawn floorSpawnScript; // Reference to the FloorSpawn script for Level 1
    public FloorSpawnLvl2 floorSpawnLvl2Script; // Reference to the FloorSpawnLvl2 script for Level 2

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
        StopSpawners();
        ClearCurrentLevel();

        currentLevelIndex = GetRandomLevelIndex();
        MovePlayerToLevel(levelPlatforms[currentLevelIndex]);

        StartSpawners();
    }

    void StopSpawners()
    {
        if (floorSpawnScript != null)
        {
            floorSpawnScript.StopSpawning();
        }

        if (floorSpawnLvl2Script != null)
        {
            floorSpawnLvl2Script.StopSpawning();
        }
    }

    void ClearCurrentLevel()
    {
        GameObject[] level1Objects = GameObject.FindGameObjectsWithTag("Level1");
        foreach (GameObject obj in level1Objects)
        {
            Destroy(obj);
        }

        GameObject[] level2Objects = GameObject.FindGameObjectsWithTag("Level2");
        foreach (GameObject obj in level2Objects)
        {
            Destroy(obj);
        }
    }

    int GetRandomLevelIndex()
    {
        return Random.Range(0, levelPlatforms.Length);
    }

    void MovePlayerToLevel(GameObject levelPlatform)
    {
        Vector3 newPosition = levelPlatform.transform.position + Vector3.up * 1.5f;
        playerTransform.position = newPosition;
    }

    void StartSpawners()
    {
        if (currentLevelIndex == 0 && floorSpawnScript != null)
        {
            floorSpawnScript.ResetSpawner();
            floorSpawnScript.enabled = true;
        }
        else if (currentLevelIndex == 1 && floorSpawnLvl2Script != null)
        {
            floorSpawnLvl2Script.ResetSpawner();
            floorSpawnLvl2Script.enabled = true;
        }
    }
}
