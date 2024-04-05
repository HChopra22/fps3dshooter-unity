using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//triggering the demo scene to appear
public class DemoScene : MonoBehaviour { 
    
    [Header ("Demo loading variables")]
    public string sceneLoad;
    private float timer = 20f;

    //If the timer hits 0 then load the demo scene
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SceneManager.LoadScene("DemoScene");
        }
    }
}
