/*using System;
using MySql.Data.MySqlClient;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public InputField playerNameField;
    public Text ScoreDisplay;
    public Button SaveButton;

    private string dbConnectionString;
    private int FinalScore;

    void Awake()
    {

        FinalScore = 25;

        // Show the player's score
        ScoreDisplay.text = "Score: " + FinalScore.ToString();

        // Set up the database connection string
        dbConnectionString = "Server=your_server_address;Database=unitydb;User ID=your_username;Password=your_password;Pooling=true;";

        // Attach a listener to the save button
        SaveButton.onClick.AddListener(SaveScore);
    }

    void SaveScore()
    {
        string playerName = playerNameField.text;

        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogError("Player name cannot be empty!");
            return;
        }

        // Save the player's name and score to the database
        StoreData(playerName, FinalScore);
    }

    private void StoreData(string playerName, int score)
    {
        using (var conn = new MySqlConnection(dbConnectionString))
        {
            try
            {
                conn.Open();
                string query = @"
                    INSERT INTO player_scores (score, player_name)
                    VALUES (@score, @name);";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@score", score);
                    cmd.Parameters.AddWithValue("@name", playerName);
                    cmd.ExecuteNonQuery();
                    Debug.Log("Score saved: " + playerName + " - " + score);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Error occurred: " + e.Message);
            }
            finally
            {
                conn.Close();
                Debug.Log("Database connection closed.");
            }
        }
    }
}
sadly dll extention file conflicted 
*/