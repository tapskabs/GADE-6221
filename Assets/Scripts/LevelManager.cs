using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public FloorSpawn floorSpawnScript;
    public FloorSpawnLvl2 floorSpawnLvl2Script;
    public float levelInterval = 20f;

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
            }
            else
            {
                floorSpawnLvl2Script.StopSpawning();
                floorSpawnScript.StartSpawning();
            }

            isLevel1Active = !isLevel1Active;
        }
    }
}
