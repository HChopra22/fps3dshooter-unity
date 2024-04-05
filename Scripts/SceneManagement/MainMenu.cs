using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //binded to the play button in the UI which uses the Unity Scene manager to load the next scene after the menu
   public void PlayGame ()
    {
        //LevelScript.LoadNextLevel();
        SceneManager.LoadScene(1);
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    //Uses simple system application quit to quit the game from the button
    public void QuitGame ()
    {
        Debug.Log("GameQuit");
        Application.Quit();
    }


}
