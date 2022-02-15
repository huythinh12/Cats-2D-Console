using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SettingMenu : MonoBehaviour
{
    Resolution[] resolutions;
    [SerializeField]
    Dropdown resolutionDropDown,graphics;
    [SerializeField]
    Slider music, sfx;

    int currentResolutionIndex = 0;
    int currentQualityLevel = 0 ;
    private void Start()
    {
        currentQualityLevel = QualitySettings.GetQualityLevel();
        SetUpResolution();
        Reset();
    }

    private void SetUpResolution()
    {
        resolutions = Screen.resolutions;
        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();
        
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();
    }

    public void Reset()
    {
        music.value = 0.2f;
        sfx.value = 0.3f;
        graphics.value = currentQualityLevel;
        SetResolution(currentResolutionIndex);
        resolutionDropDown.value = currentResolutionIndex;
      
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    public void SaveAndExit()
    {
        SoundManager.saveCurrentMusicVolume = music.value;
    }
    public void SetVolumeMusic(float volume)
    {
        PlayerPrefs.SetFloat("Music", volume);
    }

    public void SetVolumeSFX(float volume)
    {
        PlayerPrefs.SetFloat("SFX", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
