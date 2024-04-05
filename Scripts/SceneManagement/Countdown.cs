using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//Script to handle the countdown animation and logic to pause game time
public class Countdown : MonoBehaviour
{
    [Header ("Countdown Variables")]
    public static Countdown instance;
    public GameObject countDown;


    //begin to load this
    private void Awake()
    {
        instance = this;
    }

    //When the script is triggered (on game load) start the counter method
    void Start()
    {
        StartCoroutine("Counter");
    }

    void Update()
    {
    
    }

    //when the game is loaded, pause time for 5 seconds show the animation and then disable the countdown
    //return back to real time
    IEnumerator Counter()
    {
        Time.timeScale = 0;
        float pauseTime = Time.realtimeSinceStartup + 5f;
        while (Time.realtimeSinceStartup < pauseTime)
            yield return 0;
        countDown.gameObject.SetActive(false);
        Time.timeScale = 1;
    }    
}
