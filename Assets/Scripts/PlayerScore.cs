using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerScore : MonoBehaviour
{

    private int score = 0;
    public TextMeshProUGUI scoreText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            // Increase the score by 1
            score++;

            // Update the UI or display the score in some way
            Debug.Log("Score: " + score);

            // Destroy the coin
            Destroy(other.gameObject);
        }
    }
}
