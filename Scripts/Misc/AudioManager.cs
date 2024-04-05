using UnityEngine.Audio;
using UnityEngine;
using System;

//A class that takes an array of audios and manages them all in one gameObject
public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    //Whenever there is a sound in the array use these variables
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    //On load, play the background music
    private void Start()
    {
        Play("Theme");
    }

    //Play, finds the sound name and plays it in scene
    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
}
