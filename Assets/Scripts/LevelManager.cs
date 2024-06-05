using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public FloorSpawn floorSpawnLevel1;
    public FloorSpawn floorSpawnLevel2;
    private int currentLevel = 1;
    private int levelsCompleted = 0;

    void Start()
    {
        StartLevel(currentLevel);
    }

    void StartLevel(int level)
    {
        if (level == 1)
        {
            floorSpawnLevel1.ResetSpawner();
            floorSpawnLevel1.enabled = true;
            floorSpawnLevel2.enabled = false;
        }
        else if (level == 2)
        {
            floorSpawnLevel2.ResetSpawner();
            floorSpawnLevel2.enabled = true;
            floorSpawnLevel1.enabled = false;
        }
    }

    public void OnLevelCompleted()
    {
        levelsCompleted++;
        if (levelsCompleted <= 2)
        {
            // Loop between level 1 and 2 initially
            currentLevel = currentLevel == 1 ? 2 : 1;
        }
        else
        {
            // Randomize level after initial loop
            currentLevel = Random.Range(1, 3);
        }
        StartLevel(currentLevel);
    }
}