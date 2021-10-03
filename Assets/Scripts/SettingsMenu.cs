using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    private Resolution[] _resolutions;

    public Dropdown resolutionDropdown;
    
    
    public void setVolume(float val)
    {
        audioMixer.SetFloat("MixerVolume", val);
    }

    public void setGraphics(int val)
    {
        QualitySettings.SetQualityLevel(val);
    }

    public void setFullscreen(bool status)
    {
        Screen.fullScreen = status;
    }

    public void setResolution(int val)
    {
        
    }

    public void Start()
    {
        _resolutions = Screen.resolutions;
        
        resolutionDropdown.ClearOptions();
        
        foreach (Resolution res in _resolutions)
        {
            // resolutionDropdown.AddOptions(res.ToString());
        }
    }
}
