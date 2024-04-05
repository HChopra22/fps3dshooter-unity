using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;

//A Script to handle multiple settings options in the main menu
public class NewSettings : MonoBehaviour
{
    [Header ("Settings Variables")]
    public AudioMixer audioMixer;
    Resolution[] resolutions;
    public Dropdown renderDropdown;

    
    //This funtion handles the array search to find the current screen resolution as well
    //as use a resolutionIndex to store all other possible resolutions to allow change
    private void Start()
    {
        int currentResolutionIndex = 0;
        resolutions = Screen.resolutions.Select(resolution => new Resolution 
        { 
            width = resolution.width, height = resolution.height 
        }).Distinct().ToArray();

        renderDropdown.ClearOptions();

        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        renderDropdown.AddOptions(options);
        renderDropdown.value = currentResolutionIndex;
        renderDropdown.RefreshShownValue();
    }

    //taking a width and height variable to provide different screen reolutions
    public void setResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    //Using an audioMixer to manipulate master volume
    public void setVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    //manipulating quality settings in the resolution index to allow changing through the UI
    public void setGraphics(int resolutionIndex)
    {
        QualitySettings.SetQualityLevel(resolutionIndex);
    }
    
    //Fullscreen toggle
    public void fullScreen (bool isfullScreen)
    {
        Screen.fullScreen = isfullScreen;
    }
}