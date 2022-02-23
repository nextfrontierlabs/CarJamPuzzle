using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sounds
{
    engineRunning,
    hitting
}
public class AudioHandler : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioSource[] audioSources;
    private void Awake()
    {
        audioSources = GetComponents<AudioSource>();
        SetAudiosToSources();
    }


    private void Events_OnStopRunningEngineSound()
    {
        StopEngineRunningSound();
    }

    private void Events_OnPlayStartEngineSound()
    {
        PlayEngineRunningSound();
    }

    private void Events_OnPlayHittingSound()
    {
        PlayHittingSound();
    }

    private void SetAudiosToSources()
    {
        audioSources[(int)Sounds.engineRunning].clip = audioClips[(int)Sounds.engineRunning];
        audioSources[(int)Sounds.hitting].clip = audioClips[(int)Sounds.hitting];
    }
    public void PlayEngineRunningSound()
    {
       
        audioSources[(int)Sounds.engineRunning].loop = true;
        if (PlayerPrefs.GetInt(GameConstants.music) == 1)
            audioSources[(int)Sounds.engineRunning].Play();
    }
    public void StopEngineRunningSound()
    {
        audioSources[(int)Sounds.engineRunning].Stop();
    }
    public void PlayHittingSound()
    {
        if (PlayerPrefs.GetInt(GameConstants.music) == 1)
            audioSources[(int)Sounds.hitting].Play();
    }

}
