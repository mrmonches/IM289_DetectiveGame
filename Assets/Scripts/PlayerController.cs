using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

// By Nolan

public class PlayerController : MonoBehaviour
{
    private bool isSelecting;
    private bool inItemViewer;

    [SerializeField] private PlayerLocation CurrentStation;
    [SerializeField] private Camera SceneCamera;
    private Vector3 lastPosition;

    [SerializeField] private float CastDistance;

    [SerializeField] private LayerMask LevelMask, EvidenceMask, CabinetMask, FoldersMask, StackMask, TypewriterMask;

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

    public bool InItemViewer { get => inItemViewer; set => inItemViewer = value; }
    public EvidenceController EvidenceController { get => _evidenceController; set => _evidenceController = value; }

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _playerControls.DefaultControls.Enable();

        _playerControls.DefaultControls.RightClick.started += rightClickAction_started;

        _playerControls.DefaultControls.LeftClick.started += leftClickAction_started;
        _playerControls.DefaultControls.LeftClick.canceled += leftClickAction_canceled;

        //_playerControls.DefaultControls.Scroll.started += Scroll_started;
        //_playerControls.DefaultControls.Scroll.canceled += Scroll_canceled;
    }

    private void leftClickAction_started(InputAction.CallbackContext obj)
    {
        // Allows left-click to not call the following code if set to EvidenceBoard
        if (CurrentStation == PlayerLocation.EvidenceBoard)
        {
            isSelecting = true;

            if (EvidenceController != null)
            {
                if (_menuBehavior != null && _menuBehavior.GetCardMenuStatus())
                {
                    _menuBehavior.SetCardMenuStatus(false);

                    _menuBehavior = null;
                }

                EvidenceController.IsHeld = true;
            }  
        }

        //Quinn wrote this. Nolan made edits (9/22/24)
        //For opening/closing the filing cabinet
        if (CurrentStation == PlayerLocation.FilingCabinet /*&& !InItemViewer*/)
        {
            RaycastHit hitFolder;
            /*
            if (Physics.Raycast(SceneCamera.ScreenPointToRay(mousePosition), out hitCabinet, CastDistance, CabinetMask))
            {
                _cabinetController = hitCabinet.collider.gameObject.GetComponent<CabinetController>();

                _cabinetController.GetOpenClose();

                if (!_cabinetController.IsOpened)
                {
                    _cabinetController = null;
                }
            }*/
            if(Physics.Raycast(SceneCamera.ScreenPointToRay(mousePosition), out hitFolder, CastDistance, FoldersMask))
            {
                _folderController = hitFolder.collider.gameObject.GetComponent<FolderController>();

                _folderController.OpenCloseFile();

                InItemViewer = true;

                Debug.Log("Raycast his the folder");
            }
        }
        
            RaycastHit hitTypewriter;
            if (Physics.Raycast(SceneCamera.ScreenPointToRay(mousePosition), out hitTypewriter, CastDistance, TypewriterMask))
            {
                _typeWriterController.GetComponent<TypeWriterController>().GetShowCanvas();
            }
        
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

                _menuBehavior = hitObject.MenuBehavior;

                _menuBehavior.SetCardMenuStatus(true);
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

    //private void OnScroll(InputValue scrollFloat)
    //{
    //    if (CurrentStation == PlayerLocation.EvidenceBoard)
    //    {
    //        _cameraController.ZoomBoardCamera(scrollFloat.Get<float>());
    //    }
    //}
    
    //private void Scroll_started(InputAction.CallbackContext obj)
    //{
    //    if (CurrentStation == PlayerLocation.EvidenceBoard)
    //    {
    //        _cameraController.IsScrolling = true;
    //    }
    //}

    //private void Scroll_canceled(InputAction.CallbackContext obj)
    //{
    //    if (CurrentStation == PlayerLocation.EvidenceBoard)
    //    {
    //        _cameraController.IsScrolling = false;
    //    }
    //}

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

            EvidenceController = hit.collider.gameObject.GetComponent<EvidenceController>();

            if (!EvidenceController.IsHover)
            {
                EvidenceController.IsHover = true;
            }
        }
        else if (Physics.Raycast(SceneCamera.ScreenPointToRay(mousePosition), out hit, CastDistance, StackMask))
        {
            if (_stackManager == null)
            {
                _stackManager = hit.collider.gameObject.GetComponent<EvidenceStackManager>();
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

