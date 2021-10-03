using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    private Resolution[] _resolutions;

    public TMP_Dropdown resolutionDropdown;

    public Toggle toggle;
    
    
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
        Resolution res = _resolutions[val];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen, res.refreshRate);
    }

    public void Start()
    {
        _resolutions = Screen.resolutions;
        
        resolutionDropdown.ClearOptions();

        List<String> tmp = new List<string>();
        int actualIndex = 0;
        for (int i = 0; i < _resolutions.Length; i++)
        {
            tmp.Add(_resolutions[i].width + "x" + _resolutions[i].height + "@" + _resolutions[i].refreshRate + "hz");

            if (_resolutions[i].ToString() == Screen.currentResolution.ToString())
            {
                actualIndex = i;
            }
        }
        
        resolutionDropdown.AddOptions(tmp);
        resolutionDropdown.value = actualIndex;
        resolutionDropdown.RefreshShownValue();


        toggle.isOn = Screen.fullScreen;
    }
}
