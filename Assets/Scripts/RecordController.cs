using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class RecordController : MonoBehaviour
{
    [SerializeField] private FMOD.Studio.EventInstance music;

    private void Awake()
    {
        music = FMODUnity.RuntimeManager.CreateInstance("event:/JazzEvent");
        music.start();
    }
    public void pauseMusic()
    {
       music.setPaused(true);
    }
    public void unpauseMusic()
    {
        music.setPaused(false);
    }
}
