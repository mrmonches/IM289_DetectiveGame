using FMOD.Studio;
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
    private RecordController _recordController;
    private LampController _lampController;
    private bool paused = false;
  
    private void OnEnable()
    {
        switch(Name)
        {
            case InteractionName.Phone: 
                _phoneManager = GetComponent<PhoneManager>(); 
                break;
            case InteractionName.RecordPlayer: 
                _studioEventEmitter = GetComponent<StudioEventEmitter>(); 
                _recordController = GetComponent<RecordController>();
                break;
            case InteractionName.Lamp:
                _lampController = GetComponent<LampController>();
                break;
            default: break;
        }
    }

    private enum InteractionName
    {
        Phone,
        RecordPlayer,
        Lamp
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
                if (!paused)
                {
                    _recordController.pauseMusic();
                    paused = true;
                }
                else
                {
                    _recordController.unpauseMusic();
                    paused = false;
                }
                break;
            case InteractionName.Lamp:

                    
            default: break;
        }
    }
}
