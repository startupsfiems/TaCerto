using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    [Header("Audio Button Stuff")]
    public Image audioButtonImage;
    public Sprite audioOnImage;
    public Sprite audioOffImage;
    private int audioOn;

    [Header("Audio Stuff")]
    public AudioManager instance;

    public AudioMixerGroup mixerGroup;

    public Sound[] audioSources;

    private void Start()
    {
        if (PlayerPrefs.HasKey(GameConstants.AUDIO_ON))
        {
            audioOn = PlayerPrefs.GetInt(GameConstants.AUDIO_ON);
        }
        else
        {
            audioOn = 1;
        }

        SetAudioImage(audioOn == 1 ? true : false);
        
    }

    public void PlaySound(string sound)
    {
        if(audioOn == 1)
        {
            Sound s = Array.Find(audioSources, item => item.name == sound);
            if (s == null)
            {
                Debug.Log("Som: " + name + " não encontrado.");
                return;
            }
            AudioSource source = s.source;
            source.PlayOneShot(source.clip, source.volume);
        }
    }

    public void SwitchAudio()
    {
        if (audioOn == 1)
            audioOn = 0;
        else
            audioOn = 1;

        SetAudioImage(audioOn == 1 ? true : false);

        PlayerPrefs.SetInt(GameConstants.AUDIO_ON, audioOn);
        PlayerPrefs.Save();
    }

    private void SetAudioImage(bool v)
    {
        if (v)
            audioButtonImage.sprite = audioOnImage;
        else
            audioButtonImage.sprite = audioOffImage;
    }
}
