using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private InteractionName Name;

    private PhoneManager _phoneManager;
    private StudioEventEmitter _studioEventEmitter;
    private PlayerController _playerController;
    private AudioManager _audioManager;

    private void OnEnable()
    {
        switch(Name)
        {
            case InteractionName.Phone: _phoneManager = GetComponent<PhoneManager>(); break;
            case InteractionName.RecordPlayer: _studioEventEmitter = GetComponent<StudioEventEmitter>();_playerController = GetComponent<PlayerController>();_audioManager = GetComponent<AudioManager>(); break;

            default: break;
        }
    }

    private enum InteractionName
    {
        Phone,
        RecordPlayer
    }

    public void CallInteraction()
    {
        switch (Name)
        {
            case InteractionName.Phone:
                if (_phoneManager.IsRinging)
                    _phoneManager.PickupPhone();
                break;
            case InteractionName.RecordPlayer:
                if (_playerController.MusicPaused)
                    _audioManager.pauseMusic(_studioEventEmitter);
                else
                    _audioManager.unpauseMusic(_studioEventEmitter);
                break;
                    
            default: break;
        }
    }
}
