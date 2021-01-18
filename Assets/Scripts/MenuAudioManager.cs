using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuAudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private static bool soundPlaying = false;
    private bool updateNeeded;
    // Start is called before the first frame update
    void Awake()
    {
        // Setup volume preferences if they aren't set up already
        if (!PlayerPrefs.HasKey("MasterVolume")) PlayerPrefs.SetInt("MasterVolume", 1);
        if (!PlayerPrefs.HasKey("MusicVolume")) PlayerPrefs.SetInt("MusicVolume", 1);
        if (!PlayerPrefs.HasKey("SfxVolume")) PlayerPrefs.SetInt("SfxVolume", 1);

        // Do this for other playerprefs that must also be initialized
        if (!PlayerPrefs.HasKey("SfxVolume")) PlayerPrefs.SetInt("SfxVolume", 1);
        if (!PlayerPrefs.HasKey("GameLevel")) PlayerPrefs.SetInt("ControlSystem", 1);


        updateNeeded = false;
        sounds[0].source = gameObject.AddComponent<AudioSource>();
        sounds[0].source.clip = sounds[0].clip;

        sounds[0].source.volume = sounds[0].volume;
        sounds[0].source.loop = sounds[0].loop;

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        if (!soundPlaying)
        {
            PlayMusic("Music");
            soundPlaying = true;
        }
        else if (SceneManager.GetActiveScene().name != "MainMenu" && SceneManager.GetActiveScene().name != "GarageMenu")
        {
            sounds[0].source.volume = 0;
            soundPlaying = false;
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu" && SceneManager.GetActiveScene().name != "GarageMenu")
        {
            sounds[0].source.volume = 0;
        }
        else 
        {
            sounds[0].source.volume = PlayerPrefs.GetFloat("MusicVolume");
        }
        
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.volume = PlayerPrefs.GetFloat("MusicVolume");
        s.source.Play();
    }

    public void SetMenuSoundVolume(float f)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.volume = PlayerPrefs.GetFloat("MusicVolume");
        s.source.Play();
        s.source.volume = f;
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.volume = PlayerPrefs.GetFloat("SfxVolume");
        s.source.Play();
    }
}
