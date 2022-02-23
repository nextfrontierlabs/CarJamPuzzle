using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GlobalSounds
{
    bgMusic,
    btnClicks
}
public class SoundController : MonoBehaviour
{
    public AudioSource[] audioSources;
    private void Awake()
    {
        audioSources = GetComponents<AudioSource>();
    }
    private void OnEnable()
    {
        Events.OnChangeMusicState += Events_OnChangeMusicState;
        Events.OnPlayClickBtn += Events_OnPlayClickBtn;
        Events_OnChangeMusicState();
    }

    private void Events_OnPlayClickBtn()
    {
        PlayClickSound();
    }

    private void Events_OnChangeMusicState()
    {
        if(PlayerPrefs.GetInt(GameConstants.music,1)==1)
        {
            for (int i = 0; i < audioSources.Length; i++)
            {
                audioSources[i].mute = false;
            }
        }
        else
        {
            for (int i = 0; i < audioSources.Length; i++)
            {
                audioSources[i].mute = true;
            }
        }
    }

    private void PlayClickSound()
    {
        if (PlayerPrefs.GetInt(GameConstants.music,1) == 1)
            audioSources[(int)GlobalSounds.btnClicks].Play();
    }
    private void OnDisable()
    {
        Events.OnChangeMusicState -= Events_OnChangeMusicState;
        Events.OnPlayClickBtn -= Events_OnPlayClickBtn;
    }
}
