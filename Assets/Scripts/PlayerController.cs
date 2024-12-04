using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using FMODUnity;
using Unity.VisualScripting.Antlr3.Runtime;

// By Nolan

public class PlayerController : MonoBehaviour
{
    private bool isSelecting;
    [SerializeField] private bool inItemViewer;
    private bool isConnecting;
    private bool isCutting;

    [SerializeField] private PlayerLocation CurrentStation;
    [SerializeField] private Camera SceneCamera;
    private Vector3 lastPosition;

    [SerializeField] private float CastDistance;

    [SerializeField] private LayerMask LevelMask, EvidenceMask, CabinetMask, FoldersMask, StackMask, TypewriterMask, UIMask, LineMask, InteractionMask, TrashMask;

    private Vector3 mousePosition;

    private EvidenceController _evidenceController;

    private EvidenceStackManager _stackManager;

    [SerializeField] private YarnController _yarnController;

    [SerializeField] private GameObject _cabinetObject;

    private PlayerControls _playerControls;

    private CabinetController _cabinetController;

    private FolderController _folderController;

    private EvidenceBoardManager _boardManager;

    [SerializeField] private GameObject _typeWriterController;

    [SerializeField] private CameraController _cameraController;

    [SerializeField] private PhoneManager _phoneManager;

    private SystemManager _systemManager;

    private EvidenceCardMenuBehavior _menuBehavior;

    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private EventReference ClickSound;

    [SerializeField] private AudioClip MainMusic;

    private GameObject interactionObject;

    private bool paperopen;
    private TypeWriterController typeWriterController;
    private DocumentTurnPage _documentTurnPage;
    private string openDoc;
    private GameObject openEvidence;
    [SerializeField] private GameObject pausemenu;
    private bool paused = false;

    private TitleFadeAway _titleFadeAway;
    public bool InItemViewer { get => inItemViewer; set => inItemViewer = value; }
    public EvidenceController EvidenceController { get => _evidenceController; set => _evidenceController = value; }
    public bool IsConnecting { get => isConnecting; set => isConnecting = value; }
    public Vector3 GetMousePosition { get => mousePosition; set => mousePosition = value; }

    //private bool musicOn = true;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _playerControls.DefaultControls.Enable();

        _playerControls.DefaultControls.RightClick.started += rightClickAction_started;
        _playerControls.DefaultControls.RightClick.canceled += RightClickAction_canceled;

        _playerControls.DefaultControls.LeftClick.started += leftClickAction_started;
        _playerControls.DefaultControls.LeftClick.canceled += leftClickAction_canceled;
        _playerControls.DefaultControls.Quit.started += Quit_started;
        _playerControls.DefaultControls.Quit.canceled += Quit_canceled;
        _titleFadeAway = FindObjectOfType<TitleFadeAway>();


        _boardManager = FindObjectOfType<EvidenceBoardManager>();
        _systemManager = FindObjectOfType<SystemManager>();
        typeWriterController = FindObjectOfType<TypeWriterController>();
    }

    private void Quit_canceled(InputAction.CallbackContext obj)
    {
        
    }

    private void Quit_started(InputAction.CallbackContext obj)
    {
       
        if (paperopen == true)
        {
            if (openDoc == "TypeWriter")
            {
                typeWriterController.BackToDesk();
            }
            else if (openDoc == "Doc")
            {
                _folderController.CloseFile();
            }


            updatePaperOpen(false, "null");

        }
        else if (paused == true)
        {
            pausemenu.gameObject.SetActive(false);
            _cameraController.updatePause(false);

            paused = false;
        }
        else
        {
            pausemenu.gameObject.SetActive(true);
            _cameraController.updatePause(true);
            paused = true;

        }
    }
    public void unPause()
    {
        pausemenu.gameObject.SetActive(false);
        _cameraController.updatePause(false);
        paused = false;
    }
    public void updatePaperOpen(bool input, string inputS)
    {
        if (input == true)
        {
            paperopen = true;
        }
        else
        {
            paperopen = false;
        }
        openDoc = inputS;

    }

    public void getOpenDoc(GameObject input)
    {
        openEvidence = input;
    }


    private void leftClickAction_started(InputAction.CallbackContext obj)
    {
        _titleFadeAway.disabletitle();
        switch (CurrentStation)
        {
            case PlayerLocation.EvidenceBoard:
                if (!isConnecting)
                {
                    isSelecting = true;

                    if (EvidenceController != null)
                    {
                        EvidenceController.IsHeld = true;

                        AudioManager.instance.PlayOneShot(ClickSound, SceneCamera.transform.position);
                    }
                }
                break;

            case PlayerLocation.Desk:
                RaycastHit hit;
                if (Physics.Raycast(SceneCamera.ScreenPointToRay(mousePosition), out hit, CastDistance, TypewriterMask))
                {
                    _typeWriterController.GetComponent<TypeWriterController>().GetShowCanvas();

                    _titleFadeAway.disabletitle();

                    AudioManager.instance.PlayOneShot(ClickSound, SceneCamera.transform.position);
                }
                else if (Physics.Raycast(SceneCamera.ScreenPointToRay(mousePosition), out hit, CastDistance, InteractionMask))
                {
                    interactionObject = hit.transform.gameObject;
                }
                else if (Physics.Raycast(SceneCamera.ScreenPointToRay(mousePosition), out hit, CastDistance, FoldersMask) && !typeWriterController.GetTypewriterStatus())
                {
                    _folderController = hit.collider.gameObject.GetComponent<FolderController>();

                    AudioManager.instance.PlayOneShot(ClickSound, SceneCamera.transform.position);

                    _folderController.OpenFile();

                    InItemViewer = true;

                    Debug.Log("Raycast his the folder");

                    updatePaperOpen(true, "Doc");
                }

                //Quinn. This is here in order to access the intro letter, which is on the desk. 
                    //RaycastHit letterHit;
                    //if (Physics.Raycast(SceneCamera.ScreenPointToRay(mousePosition), out letterHit, CastDistance, FoldersMask) && )
                    //{
                    //    _folderController = letterHit.collider.gameObject.GetComponent<FolderController>();

                    //    AudioManager.instance.PlayOneShot(ClickSound, SceneCamera.transform.position);

                    //    _folderController.OpenFile();

                    //    InItemViewer = true;

                    //    Debug.Log("Raycast his the folder");

                    //    updatePaperOpen(true, "Doc");
                    //}
                break;

            case PlayerLocation.FilingCabinet:
                if (!InItemViewer)
                {
                    RaycastHit hitFolder;

                    if (Physics.Raycast(SceneCamera.ScreenPointToRay(mousePosition), out hitFolder, CastDistance, FoldersMask))
                    {
                        _folderController = hitFolder.collider.gameObject.GetComponent<FolderController>();

                        AudioManager.instance.PlayOneShot(ClickSound, SceneCamera.transform.position);

                        _folderController.OpenFile();

                        InItemViewer = true;

                        Debug.Log("Raycast his the folder");

                        updatePaperOpen(true,"Doc");
                        
                    }
                }

                break;

            default: break;
        }


        // Allows left-click to not call the following code if set to EvidenceBoard
        //if (CurrentStation == PlayerLocation.EvidenceBoard)
        //{
        //    isSelecting = true;

        //    if (EvidenceController != null)
        //    {
        //        if (_menuBehavior != null && _menuBehavior.GetCardMenuStatus())
        //        {
        //            _menuBehavior.SetCardMenuStatus(false);

        //            _menuBehavior = null;
        //        }

        //        EvidenceController.IsHeld = true;
        //    }  
        //}

        //Quinn wrote this. Nolan made edits (10/20/24)
        //For opening/closing the filing cabinet
        //if (CurrentStation == PlayerLocation.FilingCabinet && !InItemViewer)
        //{
        //    RaycastHit hitFolder;

        //    if(Physics.Raycast(SceneCamera.ScreenPointToRay(mousePosition), out hitFolder, CastDistance, FoldersMask))
        //    {
        //        _folderController = hitFolder.collider.gameObject.GetComponent<FolderController>();

        //        _folderController.OpenCloseFile();

        //        InItemViewer = true;

        //        Debug.Log("Raycast his the folder");
        //    }
        //}
        
            //RaycastHit hitTypewriter;
            //if (Physics.Raycast(SceneCamera.ScreenPointToRay(mousePosition), out hitTypewriter, CastDistance, TypewriterMask))
            //{
            //    _typeWriterController.GetComponent<TypeWriterController>().GetShowCanvas();
            //}
    }

    private void leftClickAction_canceled(InputAction.CallbackContext obj)
    {
        if (CurrentStation == PlayerLocation.EvidenceBoard)
        {
            RaycastHit hit;

            if (Physics.Raycast(SceneCamera.ScreenPointToRay(mousePosition), out hit, CastDistance, TrashMask) && 
                isSelecting && !EvidenceController.DeleteStatus())
            { 
                DiscardScript _discardScript = hit.transform.GetComponent<DiscardScript>();

                _discardScript.DiscardCard(EvidenceController.ID, EvidenceController.gameObject);

                EvidenceController = null;
            }

            isSelecting = false;

            if (EvidenceController != null)
            {
                EvidenceController.OnPlace();
            }
        }
        else if (CurrentStation == PlayerLocation.Desk)
        {
            RaycastHit hit;

            if (Physics.Raycast(SceneCamera.ScreenPointToRay(mousePosition), out hit, CastDistance, InteractionMask))
            {
                
                if (interactionObject != null && interactionObject.Equals(hit.transform.gameObject))
                {
                    interactionObject.GetComponent<InteractionManager>().CallInteraction();
                }
            }

            if (interactionObject != null)
            {
                interactionObject = null;
            }
        }
    }

    private void rightClickAction_started(InputAction.CallbackContext obj)
    {
        if (CurrentStation == PlayerLocation.EvidenceBoard) 
        {
            if (!isSelecting)
            {
                if (_boardManager.Connections.Count > 0)
                {
                    Ray ray = SceneCamera.ScreenPointToRay(mousePosition);
                    RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

                    if (hit2D.collider != null && hit2D.transform.CompareTag("LineRenderer"))
                    {
                        //print("hitLine");

                        isCutting = true;
                    }
                }
            
                RaycastHit hit;

                if (Physics.Raycast(SceneCamera.ScreenPointToRay(mousePosition), out hit, CastDistance, EvidenceMask))
                {
                    EvidenceController hitObject = hit.collider.GetComponent<EvidenceController>();

                    if (!hitObject.IsInHand)
                    {
                        _yarnController = hitObject.GetComponent<YarnController>();

                        _yarnController.CheckLineStatus(hitObject.ChildTransform.position, hitObject.EvidenceData, hitObject.gameObject);

                        isConnecting = true;
                    }
                }
            }
        }
    }

    private void RightClickAction_canceled(InputAction.CallbackContext obj)
    {
        if (CurrentStation == PlayerLocation.EvidenceBoard)
        {
            if (_boardManager.Connections.Count > 0)
            {
                Ray ray = SceneCamera.ScreenPointToRay(mousePosition);
                RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

                if (hit2D.collider != null && hit2D.transform.CompareTag("LineRenderer") && isCutting)
                {
                    YarnCollision yc = hit2D.collider.GetComponent<YarnCollision>();
                    yc.EndCollisions();
                }
                else
                {
                    isCutting = false;
                }
            }

            if (isConnecting)
            {
                RaycastHit hit;

                if (Physics.Raycast(SceneCamera.ScreenPointToRay(mousePosition), out hit, CastDistance, EvidenceMask))
                {
                    EvidenceController hitObject = hit.collider.GetComponent<EvidenceController>();

                    if (hitObject.GetIsImageLabel)
                    {
                        hitObject = hitObject.GetParentObject.GetComponent<EvidenceController>();
                    }

                    if (!hitObject.IsInHand)
                    {
                        _yarnController.CheckLineStatus(hitObject.ChildTransform.position, hitObject.EvidenceData, hitObject.gameObject);
                    }
                }
                else
                {
                    _yarnController.ClearUnfinishedConnection();
                }

                _yarnController = null;

                isConnecting = false;
            }
        }
    }

    private void OnBoardMove(InputValue moveVector)
    {
        if (CurrentStation == PlayerLocation.EvidenceBoard && !_cameraController.GetBlendStatus())
        {
            _cameraController.MoveBoardCamera(moveVector.Get<Vector2>());
        }
        else if (CurrentStation == PlayerLocation.EvidenceBoard && _cameraController.GetBlendStatus())
        {
            _cameraController.MoveBoardCamera(Vector3.zero);
        }
    }

    private void OnMouse(InputValue mousePos)
    {
        mousePosition = mousePos.Get<Vector2>();
    }

    /// <summary>
    /// Calculates the player's last mouse position on the board
    /// </summary>
    /// <returns></returns>
    public Vector3 GetSelectedPosition()
    {
        mousePosition.z = SceneCamera.nearClipPlane;

        RaycastHit hit; 
        if (Physics.Raycast(SceneCamera.ScreenPointToRay(mousePosition), out hit, CastDistance, LevelMask))
        {
            lastPosition = hit.point;
        }

        return lastPosition;
    }

    public void AssignYarnController(YarnController yarn)
    {
        _yarnController = yarn;

        isConnecting = true;
    }

    public void UnassignYarnController()
    {
        _yarnController = null;

        isConnecting = false;
    }

    public YarnController GetYarnController()
    {
        return _yarnController;
    }

    /// <summary>
    /// Responsible for handling assigning references to evidence and handling hover functions
    /// </summary>
    private void EvidenceSelect()
    {
        RaycastHit hit;

        if (Physics.Raycast(SceneCamera.ScreenPointToRay(mousePosition), out hit, CastDistance, EvidenceMask))
        {
            if (EvidenceController != null && EvidenceController != hit.collider.gameObject.GetComponent<EvidenceController>())
            {
                EvidenceController.IsHover = false;
            }

            RaycastHit hitCardMenu;

            if (!Physics.Raycast(SceneCamera.ScreenPointToRay(mousePosition), out hitCardMenu, CastDistance, UIMask))
            {
                EvidenceController = hit.collider.gameObject.GetComponent<EvidenceController>();

                if (EvidenceController.GetIsImageLabel)
                {
                    EvidenceController = EvidenceController.GetParentObject.GetComponent<EvidenceController>();
                }

                if (!EvidenceController.IsHover)
                {
                    EvidenceController.IsHover = true;
                }
            }
        }
        else
        {
            if (EvidenceController != null)
            {
                if (EvidenceController.IsHover)
                {
                    EvidenceController.IsHover = false;
                }

                EvidenceController = null;
            }

            if (_stackManager != null)
            {
                _stackManager = null;
            }
        }
    }

    private void FixedUpdate()
    {
        switch (CurrentStation)
        {
            case PlayerLocation.Desk:
                break;
            case PlayerLocation.EvidenceBoard:
                if (!isSelecting)
                {
                    EvidenceSelect();
                }

                if (isConnecting)
                {
                    _yarnController.LineFollowMouse(GetSelectedPosition());
                }
                break;
            case PlayerLocation.FilingCabinet:
                if (!isSelecting)
                {

                }
                break;
        }

        if (CurrentStation != PlayerLocation.EvidenceBoard && isConnecting)
        {
            _yarnController.ClearUnfinishedConnection();

            UnassignYarnController();

            print("called");
        }

        if (CurrentStation != PlayerLocation.FilingCabinet && inItemViewer)
        {
            //Quinn. Commented this out because there is now a file on the desk
            //_folderController.CloseFile();
        }
    }
    /// <summary>
    /// Quinn - I don't know how switch statements work. This is fed through the Camera Controller
    /// </summary>
    public void StationSetDesk()
    {
        CurrentStation = PlayerLocation.Desk;
    }

    public void StationSetCabinet()
    {
        CurrentStation = PlayerLocation.FilingCabinet;
    }

    public void StationSetBoard()
    {
        CurrentStation = PlayerLocation.EvidenceBoard;
    }

    public bool GetPlayerStatus()
    {
        if (inItemViewer || typeWriterController.IsActive)
        {
            return false;
        }
        else 
        {
            return true;
        }
    }

    private void OnDestroy()
    {
        _playerControls.DefaultControls.RightClick.started -= rightClickAction_started;

        _playerControls.DefaultControls.LeftClick.started -= leftClickAction_started;
        _playerControls.DefaultControls.LeftClick.canceled -= leftClickAction_canceled;
    }
}

public enum PlayerLocation
{
    Desk,
    EvidenceBoard,
    FilingCabinet
}


