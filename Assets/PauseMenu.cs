using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
     public static bool GameIspaused = false;
     public GameObject pauseMenuUI;
   
    


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIspaused) 
            {
                Resume();
            
            }
            else
            {
                Pause();
            }

        }
    }

   public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIspaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIspaused = true;

        
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Start Screen", LoadSceneMode.Additive);
        //SceneManager.LoadScene("Start Screen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
