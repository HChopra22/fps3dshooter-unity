using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//The script to handle the buttons for the game over scene
public class GameOver : MonoBehaviour
{
    //When this scene is loaded, make the cursor visible and not locked
    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
  
    //button to retry the game
    public void loadGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    //button to return to main menu
    public void loadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
   
    //button to quit the game
    public void quitGame()
    {
        Application.Quit();
    }
}
