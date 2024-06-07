using System.Collections;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public FloorSpawn floorSpawnScript;
    public FloorSpawnLvl2 floorSpawnLvl2Script;
    public float levelInterval = 20f;
    public TextMeshProUGUI levelCounterText; // Reference to the TMPro text element

    private bool isLevel1Active = true;
    private int levelCount = 0;

    void Start()
    {
        if (floorSpawnScript == null || floorSpawnLvl2Script == null)
        {
            Debug.LogError("FloorSpawnScript or FloorSpawnLvl2Script is not assigned in the inspector.");
            return;
        }

        // Start the first level's platform spawner
        floorSpawnScript.StartSpawning();
        UpdateLevelCounter();
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
            levelCount++;
            UpdateLevelCounter();
        }
    }

    void UpdateLevelCounter()
    {
        levelCounterText.text = "Levels Cleared: " + levelCount;
    }
}
