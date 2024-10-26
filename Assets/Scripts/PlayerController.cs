using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

// By Nolan

public class PlayerController : MonoBehaviour
{
    private bool isSelecting;
    [SerializeField] private bool inItemViewer;
    private bool isConnecting;

    [SerializeField] private PlayerLocation CurrentStation;
    [SerializeField] private Camera SceneCamera;
    private Vector3 lastPosition;

    [SerializeField] private float CastDistance;

    [SerializeField] private LayerMask LevelMask, EvidenceMask, CabinetMask, FoldersMask, StackMask, TypewriterMask, UIMask;

    private Vector3 mousePosition;

    private EvidenceController _evidenceController;

    private EvidenceStackManager _stackManager;

    [SerializeField] private YarnController _yarnController;

    [SerializeField] private GameObject _cabinetObject;

    private PlayerControls _playerControls;

    private CabinetController _cabinetController;

    private FolderController _folderController;

    [SerializeField] private GameObject _typeWriterController;

    [SerializeField] private CameraController _cameraController;

    private EvidenceCardMenuBehavior _menuBehavior;

    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip ClickClip;

    private TitleFadeAway _titleFadeAway;
    public bool InItemViewer { get => inItemViewer; set => inItemViewer = value; }
    public EvidenceController EvidenceController { get => _evidenceController; set => _evidenceController = value; }
    public bool IsConnecting { get => isConnecting; set => isConnecting = value; }

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _playerControls.DefaultControls.Enable();

        _playerControls.DefaultControls.RightClick.started += rightClickAction_started;

        _playerControls.DefaultControls.LeftClick.started += leftClickAction_started;
        _playerControls.DefaultControls.LeftClick.canceled += leftClickAction_canceled;
        _titleFadeAway= FindObjectOfType<TitleFadeAway>();
    }

    private void leftClickAction_started(InputAction.CallbackContext obj)
    {
        _titleFadeAway.disabletitle();
        switch (CurrentStation)
        {
            case PlayerLocation.EvidenceBoard:
                isSelecting = true;

                if (_menuBehavior != null && _menuBehavior.GetCardMenuStatus())
                {
                    RaycastHit hitCardMenu;
                    if (!Physics.Raycast(SceneCamera.ScreenPointToRay(mousePosition), out hitCardMenu, CastDistance, UIMask)) 
                    {
                        _menuBehavior.SetCardMenuStatus(false);

                        _menuBehavior = null;
                    }
                }

                if (EvidenceController != null)
                {
                    EvidenceController.IsHeld = true;

                    _audioSource.PlayOneShot(ClickClip);
                }
                break;

            case PlayerLocation.Desk:
                RaycastHit hitTypewriter;
                if (Physics.Raycast(SceneCamera.ScreenPointToRay(mousePosition), out hitTypewriter, CastDistance, TypewriterMask))
                {
                    _typeWriterController.GetComponent<TypeWriterController>().GetShowCanvas();

                    _titleFadeAway.disabletitle();

                    _audioSource.PlayOneShot(ClickClip);
                }

                break;

            case PlayerLocation.FilingCabinet:
                if (!InItemViewer)
                {
                    RaycastHit hitFolder;

                    if (Physics.Raycast(SceneCamera.ScreenPointToRay(mousePosition), out hitFolder, CastDistance, FoldersMask))
                    {
                        _folderController = hitFolder.collider.gameObject.GetComponent<FolderController>();

                        _audioSource.PlayOneShot(ClickClip);

                        _folderController.OpenCloseFile();

                        InItemViewer = true;

                        Debug.Log("Raycast his the folder");
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
            isSelecting = false;

            if (EvidenceController != null)
            {
                EvidenceController.OnPlace();
            }
        }
    }

    private void rightClickAction_started(InputAction.CallbackContext obj)
    {
        if (CurrentStation == PlayerLocation.EvidenceBoard) 
        {
            RaycastHit hit;

            if (Physics.Raycast(SceneCamera.ScreenPointToRay(mousePosition), out hit, CastDistance, EvidenceMask))
            {
                EvidenceController hitObject = hit.collider.GetComponent<EvidenceController>();

                if (!hitObject.IsInHand)
                {
                    if (_menuBehavior == null)
                    {
                        _menuBehavior = hitObject.MenuBehavior;

                        _menuBehavior.SetCardMenuStatus(true);
                    }
                    else if (_menuBehavior != null && _menuBehavior != hitObject)
                    {
                        _menuBehavior.SetCardMenuStatus(false);

                        _menuBehavior = hitObject.MenuBehavior;

                        _menuBehavior.SetCardMenuStatus(true);
                    }
                }
            }
            else
            {
                if (_menuBehavior != null && _menuBehavior.GetCardMenuStatus())
                {
                    _menuBehavior.SetCardMenuStatus(false);

                    _menuBehavior = null;
                }
            }
        }
    }

    private void OnBoardMove(InputValue moveVector)
    {
        if (CurrentStation == PlayerLocation.EvidenceBoard)
        {
            _cameraController.MoveBoardCamera(moveVector.Get<Vector2>());
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
            _folderController.FileControl.CloseFile();
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

