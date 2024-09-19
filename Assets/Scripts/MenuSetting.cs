using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSettings : MonoBehaviour
{
    public Slider masterVolumeSlider;
    public Slider backgroundVolumeSlider;
    public Slider efxVolumeSlider;
    private AudioManager audioManager;
    void Start()
    {
        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        backgroundVolumeSlider.onValueChanged.AddListener(SetBackgroundVolume);
        efxVolumeSlider.onValueChanged.AddListener(SetEfxVolume);

        masterVolumeSlider.value = AudioManager.instance.masterVolume;
        backgroundVolumeSlider.value = AudioManager.instance.backgroundVolume;
        efxVolumeSlider.value = AudioManager.instance.efxVolume;
    }
    public void Home()
    {
        AudioManager.instance.PlaySoundEffect(15);
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    void SetMasterVolume(float volume)
    {
        AudioManager.instance.SetMasterVolume(volume);
    }

    void SetBackgroundVolume(float volume)
    {
        AudioManager.instance.SetBackgroundVolume(volume);
    }

    void SetEfxVolume(float volume)
    {
        AudioManager.instance.SetEfxVolume(volume);
    }
    public void ApplySettings()
    {
        audioManager.masterVolume = masterVolumeSlider.value;
        audioManager.backgroundVolume = backgroundVolumeSlider.value;
        audioManager.efxVolume = efxVolumeSlider.value;

        PlayerPrefs.SetFloat("MasterVolume", audioManager.masterVolume);
        PlayerPrefs.SetFloat("MusicVolume", audioManager.backgroundVolume);
        PlayerPrefs.SetFloat("EfxVolume", audioManager.efxVolume);
        PlayerPrefs.Save();
    }
}

