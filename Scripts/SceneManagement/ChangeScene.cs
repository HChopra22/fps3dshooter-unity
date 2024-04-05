using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Calling the button sound effect for the demo
public class ChangeScene : MonoBehaviour
{
    [Header ("Button Audio")]
    public AudioClip sfxButton;
    private bool oneshotSfx;

    //if any key is pressed, play the button noise and return back to the main menu
    void Update()
    {
        if(Input.anyKeyDown)
        {
            if(!oneshotSfx)
            {
                AudioSource.PlayClipAtPoint(sfxButton, Vector3.zero);
                Invoke("LoadScene", 0.5f);
                oneshotSfx = true;
            }
        }
    }

    //load the main menu
    void LoadScene()
    {
        Application.LoadLevel(0);
    }
}
