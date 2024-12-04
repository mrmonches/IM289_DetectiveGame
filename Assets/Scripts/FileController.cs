using FMODUnity;
using UnityEngine;

public class FileController : MonoBehaviour
{
    //Written by Quinn
    //[SerializeField] private List<EvidenceData> heldDataFile;

    private PlayerController _playerController;

    private EvidenceStackManager _evidenceStackManager;

    private SoundManager _soundManager;

    private RedCircle _redCircle;
    private bool _isOnBoard;

    private EvidenceController _createdCard;

    [SerializeField] private EventReference DialogueEvent;

    private CardWarningScript _warningScript;

    private void OnEnable()
    {
        _playerController = FindObjectOfType<PlayerController>();

        _evidenceStackManager = FindObjectOfType<EvidenceStackManager>();

        _soundManager = FindObjectOfType<SoundManager>();

        _redCircle = GetComponentInChildren<RedCircle>(true);

        _warningScript = FindObjectOfType<CardWarningScript>();

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
            if(_isOnBoard == false)
            {
                //Turned off for FMOD
                //_soundManager.PlayWritingClip();

                _evidenceStackManager.AddToStack(evidenceData);
                _redCircle.Appear();
                _isOnBoard = true;
            }
            else if (_isOnBoard == true) 
            {
                //Deactivate card

                DestroyCreatedCard(evidenceData);
                _redCircle.Disappear();
                _isOnBoard = false;
            }
        }
        else
        {
            print("Too many cards");
            AudioManager.instance.PlayEvent(DialogueEvent);

            _warningScript.ActivateWarning();
        }

    }

    private void DestroyCreatedCard(EvidenceData evidenceData)
    {
        //_createdCard = FindObjectOfType<EvidenceController>().EvidenceData == evidenceData;
        EvidenceController[] evidenceControllers = FindObjectsOfType<EvidenceController>(true);

        foreach(EvidenceController controller in evidenceControllers)
        {
            EvidenceController thisEvidenceController = controller as EvidenceController;
            if (thisEvidenceController != null && thisEvidenceController.EvidenceData == evidenceData) 
            {
                _createdCard = thisEvidenceController;
                //This removes the card
                EvidenceCardMenuBehavior _createdCardMenuBehavior = _createdCard.GetComponentInParent<EvidenceCardMenuBehavior>(true);
                _createdCardMenuBehavior.RemoveCardFromBoard();
                
                if (_isOnBoard)
                {
                    _evidenceStackManager.RemoveFromStack(thisEvidenceController.EvidenceData, thisEvidenceController.gameObject);
                }

                break;
            }
        }

    }
}
