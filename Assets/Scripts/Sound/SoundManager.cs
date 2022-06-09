﻿using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoSingleton<SoundManager>
{
    public Sound[] sounds;

    public AudioSource tempSource;
    private void Awake()
    {
        foreach (var sound in sounds)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            sound.Init(source);
        }

        tempSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySound(string name)
    {
        Sound sound = Array.Find(sounds, (s) => s.soundName == name);
        if (sound == null)
        {
            CLog.LogWarning($"sound: {name} not found in sounds, create temp one");
            AudioClip clip = Resources.Load<AudioClip>($"Sound/SFX/{name}");
            if (clip == null)
                return;
            sound = Sound.CreateFromAudioClip(clip);
            sound.Init(tempSource);
        }
        sound.audioSource.Play();
    }
}
