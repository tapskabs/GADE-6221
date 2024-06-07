using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResetHighScoreButton : MonoBehaviour
{
    public TextMeshProUGUI playerHighScoreText;  // Reference to the high score text display

    void Start()
    {
        // Check if the high score text component is assigned
        if (playerHighScoreText == null)
        {
            Debug.LogError("TextMeshProUGUI component is not assigned in the inspector.");
            return;
        }

        // Initialize player's high score from PlayerPrefs
        int playerHighScore = PlayerPrefs.GetInt("PlayerHighScore", 0);
        playerHighScoreText.text = "Player High Score: " + playerHighScore;
    }

    public void ResetHighScore()
    {
        // Reset the player's high score to zero in PlayerPrefs
        PlayerPrefs.SetInt("PlayerHighScore", 0);
        PlayerPrefs.Save();  // Save the change

        // Update the high score text display
        playerHighScoreText.text = "Player High Score: 0";

        Debug.Log("Player's high score has been reset to zero.");
    }
}
