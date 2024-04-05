using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//A script to trigger and use the buttons on the pause menu during gameplay
public class PauseMenu : MonoBehaviour
{
    [Header ("Pause Objects")]
    public static bool GamePaused;
    public GameObject PauseMenuUI;

   //Set the properties of pause to false at start
    private void Start()
    {
        PauseMenuUI.SetActive(false);
        GamePaused = false;
    }

    /*if the escape key is pressed and the game is currently paused, resume or vice versa
     when the game is paused, stop time and show the Pause UI, else resume*/ 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
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
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
        Cursor.visible = false;
    }
    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
        Cursor.visible = true;
    }

    public void loadMenu()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
