using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public FloorSpawn floorSpawnScript;
    public FloorSpawnLvl2 floorSpawnLvl2Script;
    public float levelInterval = 20f;

    public GameObject secondBossPrefab; // Reference to the Second Boss prefab
    private GameObject currentBoss; // Reference to the current boss instance

    private bool isLevel1Active = true;

    void Start()
    {
        if (floorSpawnScript == null || floorSpawnLvl2Script == null)
        {
            Debug.LogError("FloorSpawnScript or FloorSpawnLvl2Script is not assigned in the inspector.");
            return;
        }

        // Start the first level's platform spawner
        floorSpawnScript.StartSpawning();
        StartCoroutine(LevelCycle());
    }

    IEnumerator LevelCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(levelInterval);

            if (isLevel1Active)
            {
                floorSpawnScript.StopSpawning();
                floorSpawnLvl2Script.StartSpawning();
                if (currentBoss != null)
                {
                    Destroy(currentBoss);
                }
            }
            else
            {
                floorSpawnLvl2Script.StopSpawning();
                floorSpawnScript.StartSpawning();
                SpawnSecondBoss();
            }

            isLevel1Active = !isLevel1Active;
        }
    }
<<<<<<< Updated upstream
=======

    void UpdateLevelCounter()
    {
        levelCounterText.text = "Levels Cleared: " + levelCount;
    }

    void SpawnSecondBoss()
    {
        Vector3 spawnPosition = new Vector3(0, 1, 10); // Adjust this position as needed
        currentBoss = Instantiate(secondBossPrefab, spawnPosition, Quaternion.identity);
    }
>>>>>>> Stashed changes
}
