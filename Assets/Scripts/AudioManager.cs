using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    // Start is called before the first frame update
    void Awake()
    {
        foreach(Sound s in sounds)
        {
            // Sets values of sound object in array
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        PlayMusic("Music");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.volume = PlayerPrefs.GetFloat("SfxVolume");
        s.source.Play();
    }

    public void PlayMusic(String name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.volume = PlayerPrefs.GetFloat("MusicVolume")*0.5f; // Makes music value smaller in game as in-game music audio files are loud.
        s.source.Play();
    }

}
