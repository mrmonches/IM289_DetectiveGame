using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FileController : MonoBehaviour
{
    //Written by Quinn
    //[SerializeField] private List<EvidenceData> heldDataFile;

    private PlayerController _playerController;

    private EvidenceStackManager _evidenceStackManager;

    private SoundManager _soundManager;

    private RedCircle _redCircle;
    private bool _isOnBoard = false;

    private EvidenceController _createdCard;

    private void OnEnable()
    {
        _playerController = FindObjectOfType<PlayerController>();

        _evidenceStackManager = FindObjectOfType<EvidenceStackManager>();

        _soundManager = FindObjectOfType<SoundManager>();

        _redCircle = GetComponentInChildren<RedCircle>(true);

        /*if( _redCircle != null )
        {
            Debug.Log("Red Circle is assigned");
        }
        else
        {
            Debug.Log("Red Circle is not assigned");
        }*/
    }

    public void SendToBoard(EvidenceData evidenceData)
    {
        if (_evidenceStackManager.GetStackCount() < 5)
        {
            if(_isOnBoard != true)
            {
                //Turned off for FMOD
                //_soundManager.PlayWritingClip();

                _evidenceStackManager.AddToStack(evidenceData);
                _redCircle.Appear();
                _isOnBoard = true;
            }
            else
            {
                //Deactivate card
                //locate created card
                
                FindCreatedCard(evidenceData);
                //destroy card
                _redCircle.Disappear();
                _isOnBoard = false;
            }
            
        }
        else
        {
            print("Too many cards");
        }

    }

    private void FindCreatedCard(EvidenceData evidenceData)
    {
        //_createdCard = FindObjectOfType<EvidenceController>().EvidenceData == evidenceData;
        EvidenceController[] evidenceControllers = FindObjectsOfType<EvidenceController>();

        foreach(EvidenceController controller in evidenceControllers)
        {
            EvidenceController thisEvidenceController = controller as EvidenceController;
            if (thisEvidenceController != null && thisEvidenceController.EvidenceData == evidenceData) 
            {
                _createdCard = thisEvidenceController;
                //This removes the card
                EvidenceCardMenuBehavior _createdCardMenuBehavior = _createdCard.GetComponentInParent<EvidenceCardMenuBehavior>();
                _createdCardMenuBehavior.RemoveCardFromBoard();
                
                break;
            }
        }

    }
}
