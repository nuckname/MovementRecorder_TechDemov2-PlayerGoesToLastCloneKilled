using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
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
        //Freeze ghost but they still move. Causing12 them to tp.
        //Time.timeScale = 1f;
        print("Resume");
        isPaused = false;
    }

    void Pause()
    {
        Debug.Log("Pause");
        pauseMenuUI.SetActive(true);

        //Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadMainMenu()
    {
        Debug.Log("MainMenu");

        //Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        
        print("2");
        SceneManager.LoadScene(0);
    }
}