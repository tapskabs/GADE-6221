using System.Collections;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public FloorSpawn floorSpawnScript;
    public FloorSpawnLvl2 floorSpawnLvl2Script;
    public float levelInterval = 20f;
    public TextMeshProUGUI levelCounterText; // Reference to the TMPro text element for current level
    public TextMeshProUGUI highScoreCounterText; // Reference to the TMPro text element for high score

    public GameObject secondBossPrefab; // Reference to the Second Boss prefab
    private GameObject currentBoss; // Reference to the current boss instance

    private bool isLevel1Active = true;
    private int levelCount = 0;
    private int highScore = 0;

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

        // Load high score from PlayerPrefs
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateHighScoreCounter();

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
            levelCount++;
            UpdateLevelCounter();

            // Check if the new score is a high score
            if (levelCount > highScore)
            {
                highScore = levelCount;
                PlayerPrefs.SetInt("HighScore", highScore);
                UpdateHighScoreCounter();
            }
        }
    }

    void UpdateLevelCounter()
    {
        levelCounterText.text = "Levels Cleared: " + levelCount;
    }

    void UpdateHighScoreCounter()
    {
        highScoreCounterText.text = "High Score: " + highScore;
    }

    void SpawnSecondBoss()
    {
        Vector3 spawnPosition = new Vector3(0, 1, 10); // Adjust this position as needed
        currentBoss = Instantiate(secondBossPrefab, spawnPosition, Quaternion.identity);
    }
}
