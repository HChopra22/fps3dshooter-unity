using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Button SFX
public class buttonSFX : MonoBehaviour
{
    [Header ("Button Audio")]
    public AudioSource myFx;
    public AudioClip clickFx;

    //play the sound once when this is called
    public void ClickSound()
    {
        myFx.PlayOneShot(clickFx);
    }
}

