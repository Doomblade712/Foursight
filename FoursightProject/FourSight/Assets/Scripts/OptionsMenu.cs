using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using System.Collections.Generic;

public class OptionsMenu : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioMixer audioMixer;
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    [Header("Graphics Settings")]
    public TMP_Dropdown qualityDropdown;
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    public Toggle vsyncToggle;

    [Header("UI References")]
    public GameObject optionsPanel;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;
    private int currentResolutionIndex = 0;

    void Start()
    {

        SetupResolutions();

        LoadSettings();

        SetupListeners();
        CloseOptionsMenu();
    }

    void SetupResolutions()
    {
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        float currentRefreshRate = (float)Screen.currentResolution.refreshRateRatio.value;
        HashSet<string> uniqueResolutions = new HashSet<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string resolutionString = resolutions[i].width + " x " + resolutions[i].height;

            if (!uniqueResolutions.Contains(resolutionString))
            {
                uniqueResolutions.Add(resolutionString);
                filteredResolutions.Add(resolutions[i]);
                options.Add(resolutionString);

                if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = filteredResolutions.Count - 1;
                }
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    void SetupListeners()
    {
        if (masterVolumeSlider) masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        if (musicVolumeSlider) musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        if (sfxVolumeSlider) sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
        if (qualityDropdown) qualityDropdown.onValueChanged.AddListener(SetQuality);
        if (resolutionDropdown) resolutionDropdown.onValueChanged.AddListener(SetResolution);
        if (fullscreenToggle) fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        if (vsyncToggle) vsyncToggle.onValueChanged.AddListener(SetVSync);
    }

    // Audio Settings
    public void SetMasterVolume(float volume)
    {
        if (audioMixer)
        {
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("MasterVolume", volume);
        }
    }

    public void SetMusicVolume(float volume)
    {
        if (audioMixer)
        {
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("MusicVolume", volume);
        }
    }

    public void SetSFXVolume(float volume)
    {
        if (audioMixer)
        {
            audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("SFXVolume", volume);
        }
    }

    // Graphics Settings
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("QualityLevel", qualityIndex);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
    }

    public void SetVSync(bool isEnabled)
    {
        QualitySettings.vSyncCount = isEnabled ? 1 : 0;
        PlayerPrefs.SetInt("VSync", isEnabled ? 1 : 0);
    }

    // Save and Load
    void LoadSettings()
    {
        // Audio
        if (masterVolumeSlider)
        {
            float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
            masterVolumeSlider.value = masterVolume;
            SetMasterVolume(masterVolume);
        }

        if (musicVolumeSlider)
        {
            float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
            musicVolumeSlider.value = musicVolume;
            SetMusicVolume(musicVolume);
        }

        if (sfxVolumeSlider)
        {
            float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
            sfxVolumeSlider.value = sfxVolume;
            SetSFXVolume(sfxVolume);
        }

        // Graphics
        if (qualityDropdown)
        {
            int qualityLevel = PlayerPrefs.GetInt("QualityLevel", QualitySettings.GetQualityLevel());
            qualityDropdown.value = qualityLevel;
            SetQuality(qualityLevel);
        }

        if (resolutionDropdown)
        {
            int resolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", currentResolutionIndex);
            resolutionDropdown.value = resolutionIndex;
        }

        if (fullscreenToggle)
        {
            bool isFullscreen = PlayerPrefs.GetInt("Fullscreen", Screen.fullScreen ? 1 : 0) == 1;
            fullscreenToggle.isOn = isFullscreen;
            SetFullscreen(isFullscreen);
        }

        if (vsyncToggle)
        {
            bool vsyncEnabled = PlayerPrefs.GetInt("VSync", 1) == 1;
            vsyncToggle.isOn = vsyncEnabled;
            SetVSync(vsyncEnabled);
        }
    }

    public void SaveSettings()
    {
        PlayerPrefs.Save();
    }

    public void ResetToDefaults()
    {
        // Reset audio to 75%
        if (masterVolumeSlider) masterVolumeSlider.value = 0.75f;
        if (musicVolumeSlider) musicVolumeSlider.value = 0.75f;
        if (sfxVolumeSlider) sfxVolumeSlider.value = 0.75f;

        // Reset graphics
        if (qualityDropdown) qualityDropdown.value = QualitySettings.GetQualityLevel();
        if (fullscreenToggle) fullscreenToggle.isOn = true;
        if (vsyncToggle) vsyncToggle.isOn = true;

        SaveSettings();
    }

    // UI Control
    public void OpenOptionsMenu()
    {
        if (optionsPanel)
            optionsPanel.SetActive(true);
    }

    public void CloseOptionsMenu()
    {
        SaveSettings();
        if (optionsPanel)
            optionsPanel.SetActive(false);
    }
}