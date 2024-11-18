using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of Audio Manager is in the scene.");
        }

        instance = this;
    }

    public void PlayOneShot(EventReference sound, Vector3 pos)
    {
        RuntimeManager.PlayOneShot(sound);
    }

    public void pauseMusic(StudioEventEmitter emitter)
    {
            emitter.EventInstance.setPaused(true);
        
    }
    public void unpauseMusic(StudioEventEmitter emitter)
    {
        emitter.EventInstance.setPaused(false);
    }

    public void PlayEvent(StudioEventEmitter emitter, EventReference sound)
    {
        emitter.EventReference = sound;
        emitter.Play();
    }

    public void StopEvent(StudioEventEmitter emitter)
    {
        emitter.Stop();
    }
}
