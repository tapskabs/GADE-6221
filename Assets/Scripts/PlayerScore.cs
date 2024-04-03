using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerScore : MonoBehaviour
{

    public int score = 0;
    public static PlayerScore instan;
    public TextMeshProUGUI scoreText;
    //public TextMeshProUGUI EndScoreText;
    public TextMeshProUGUI deathText;
    public int value;
    public static PlayerScore instance;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            // Increase the score by 1
            Score();
            
           // Debug.Log("Score: " + score);

            // Destroy the coin
            Destroy(other.gameObject);
            
            //Debug.Log("EndScore: " + endscore);
            //endScoreText.text = ("Score: " + score).ToString();
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Head"))
        {
            Score();
            // Update the UI or display the score in some way
            Debug.Log("Score: " + score);
            // endScoreText.text = ("Score: " + endScore).ToString();
            
            //Debug.Log("End Score: " + endscore);
            //endScoreText.text = ("Score: " + endscore).ToString();
        }
    }
   public void Score()
    {
        score += value;
        score++;
        scoreText.text = ("Score: " + score).ToString();

        

    }
    public void UpdateDeathScore()
    {
        deathText.text = "Score: " + score;
        PlayerPrefs.SetInt("ScoreSave", score);
    }
     void start()
    {
        value = PlayerPrefs.GetInt("ScoreSave", score);
        deathText.text = "Score: " + value;
    }

}
