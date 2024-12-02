using FMOD.Studio;
using FMODUnity;
using System.Collections.Generic;
using UnityEngine;

// Created by Nolan, additions by Jack


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    private StudioEventEmitter emitter;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of Audio Manager is in the scene.");
        }

        instance = this;

        emitter = GetComponent<StudioEventEmitter>();

        RuntimeManager.LoadBank("Master");
    }

    public void PlayOneShot(EventReference sound, Vector3 pos)
    {
        RuntimeManager.PlayOneShot(sound);
    }

    public void PlayEvent(StudioEventEmitter emitter, EventReference sound)
    {
        emitter.EventReference = sound;
        emitter.Play();
    }

    public void PlayEvent(EventReference sound)
    {
        if (!emitter.IsPlaying())
        {
            emitter.EventReference = sound;
            emitter.Play();
        }
    }

    public void StopEvent(StudioEventEmitter emitter)
    {
        emitter.Stop();
    }

    public void ControlAllEvents(bool status)
    {
        RuntimeManager.PauseAllEvents(status);
    }
}
