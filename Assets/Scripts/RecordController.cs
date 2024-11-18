using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class RecordController : MonoBehaviour
{
    public FMOD.Studio.EventInstance jazzMusic;
    public void pauseMusic()
    {
        jazzMusic.setPaused(true);
    }
    public void unpauseMusic()
    {
        jazzMusic.setPaused(false);
    }
}
