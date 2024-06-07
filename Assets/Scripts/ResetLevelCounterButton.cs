using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResetLevelCounterButton : MonoBehaviour
{
    public TextMeshProUGUI highScoreCounterText;  // Reference to the high score text display
    private const string HighScoreKey = "HighScore";  // Key to store the high score in PlayerPrefs

    public void ResetLevelCounterHighScore()
    {
        // Reset the level counter high score to zero in PlayerPrefs
        PlayerPrefs.SetInt(HighScoreKey, 0);
        PlayerPrefs.Save();  // Save the change

        // Update the high score text display
        if (highScoreCounterText != null)
        {
            highScoreCounterText.text = "High Score: 0";
        }

        Debug.Log("Level counter high score has been reset to zero.");
    }
}
