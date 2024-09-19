using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip[] soundEffects; 

    private AudioSource backgroundSource;
    private AudioSource effectsSource;

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float masterVolume = 1f;
    [Range(0f, 1f)] public float backgroundVolume = 0.5f;
    [Range(0f, 1f)] public float efxVolume = 0.5f;

    private void Start()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.SetFloat("BackgroundVolume", backgroundVolume);
        PlayerPrefs.SetFloat("EfxVolume", efxVolume);
        PlayerPrefs.Save();
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        backgroundSource = gameObject.AddComponent<AudioSource>();
        effectsSource = gameObject.AddComponent<AudioSource>();

        backgroundSource.loop = true;
        backgroundSource.volume = backgroundVolume * masterVolume;
        PlayMusic(backgroundMusic);
        masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        backgroundVolume = PlayerPrefs.GetFloat("BackgroundVolume", 0.5f);
        efxVolume = PlayerPrefs.GetFloat("EfxVolume", 0.75f);

        UpdateVolumeLevels();
    }

    public void PlayMusic(AudioClip clip)
    {
        backgroundSource.clip = clip;
        backgroundSource.Play();
    }

    public void PlaySoundEffect(int index)
    {
        if (index >= 0 && index < soundEffects.Length)
        {
            effectsSource.PlayOneShot(soundEffects[index], efxVolume * masterVolume);
        }
    }

    public void SetMasterVolume(float volume)
    {
        masterVolume = volume;
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.Save();
        UpdateVolumeLevels();
    }

    public void SetBackgroundVolume(float volume)
    {
        backgroundVolume = volume;
        PlayerPrefs.SetFloat("BackgroundVolume", backgroundVolume);
        PlayerPrefs.Save();
        UpdateVolumeLevels();
    }

    public void SetEfxVolume(float volume)
    {
        efxVolume = volume;
        PlayerPrefs.SetFloat("EfxVolume", efxVolume);
        PlayerPrefs.Save();
        UpdateVolumeLevels();
    }

    private void UpdateVolumeLevels()
    {
        backgroundSource.volume = backgroundVolume * masterVolume;
        effectsSource.volume= efxVolume * masterVolume;
    }
}

