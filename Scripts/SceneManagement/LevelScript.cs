using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//A script to give a crossfade animation when changing scenes
public class LevelScript : MonoBehaviour
{
    public Animator transition;

    //when load level is called, change level with the crossfade animation
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    //Using an IEnuerator to give a wait timer for the crossfade
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelIndex);
    }
}
