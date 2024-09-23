using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

// By Nolan

public class PlayerController : MonoBehaviour
{
    private PlayerInput _playerInput;

    private InputAction leftClickAction;
    private InputAction rightClickAction;

    private bool isSelecting;
    private bool inItemViewer;

    [SerializeField] private PlayerLocation CurrentStation;
    [SerializeField] private Camera SceneCamera;
    private Vector3 lastPosition;

    [SerializeField] private float CastDistance;

    [SerializeField] private LayerMask LevelMask, EvidenceMask, CabinetMask, FoldersMask;

    private Vector3 mousePosition;

    private EvidenceController _evidenceController;

    [SerializeField] private YarnController _yarnController;

    [SerializeField] private PlayerController _playerController;

    [SerializeField] private GameObject _cabinetObject;

    private ClickControls _clickInputs;

    private CabinetController _cabinetController;

    private FolderController _folderController;

    public bool InItemViewer { get => inItemViewer; set => inItemViewer = value; }

    private void Awake()
    {
        _clickInputs = new ClickControls();
        _clickInputs.DefaultControls.Enable();
        _clickInputs.DefaultControls.RightClick.started += rightClickAction_started;

        _clickInputs.DefaultControls.LeftClick.started += leftClickAction_started;
        _clickInputs.DefaultControls.LeftClick.canceled += leftClickAction_canceled;

        inItemViewer = false;
    }

    private void leftClickAction_started(InputAction.CallbackContext obj)
    {
        // Allows left-click to not call the following code if set to EvidenceBoard
        if (CurrentStation == PlayerLocation.EvidenceBoard)
        {
            isSelecting = true;

            if (_evidenceController != null )
            {
                _evidenceController.IsHeld = true;
            }
        }

        //Quinn wrote this. Nolan made edits (9/22/24)
        //For opening/closing the filing cabinet
        if (CurrentStation == PlayerLocation.FilingCabinet && !InItemViewer)
        {
            RaycastHit hitCabinet, hitFolder;
            
            if (Physics.Raycast(SceneCamera.ScreenPointToRay(mousePosition), out hitCabinet, CastDistance, CabinetMask))
            {
                _cabinetController = hitCabinet.collider.gameObject.GetComponent<CabinetController>();

                _cabinetController.GetOpenClose();

                if (!_cabinetController.IsOpened)
                {
                    _cabinetController = null;
                }
            }
            else if(Physics.Raycast(SceneCamera.ScreenPointToRay(mousePosition), out hitFolder, CastDistance, FoldersMask))
            {
                _folderController = hitFolder.collider.gameObject.GetComponent<FolderController>();

                _folderController.OpenCloseFile();

                InItemViewer = true;
            }
        }
    }

    private void leftClickAction_canceled(InputAction.CallbackContext obj)
    {
        if (CurrentStation == PlayerLocation.EvidenceBoard)
        {
            isSelecting = false;

            if (_evidenceController != null)
            {
                _evidenceController.OnPlace();
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

                if (_yarnController != null && _yarnController.IsConnecting)
                {
                    _yarnController.CheckLineStatus(hitObject.transform.position, hitObject.EvidenceData, hitObject.gameObject);

                    _yarnController = null;
                }
                else
                {
                    _yarnController = hit.collider.gameObject.GetComponent<YarnController>();

                    _yarnController.IsConnecting = true;

                    _yarnController.CheckLineStatus(hitObject.transform.position, hitObject.EvidenceData, hitObject.gameObject);
                }
            }
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

    /// <summary>
    /// Responsible for handling assigning references to evidence and handling hover functions
    /// </summary>
    private void EvidenceSelect()
    {
        RaycastHit hit;

        if (Physics.Raycast(SceneCamera.ScreenPointToRay(mousePosition), out hit, CastDistance, EvidenceMask))
        {
            if (_evidenceController != null && _evidenceController != hit.collider.gameObject.GetComponent<EvidenceController>())
            {
                _evidenceController.IsHover = false;
            }

            _evidenceController = hit.collider.gameObject.GetComponent<EvidenceController>();

            if (!_evidenceController.IsHover)
            {
                _evidenceController.IsHover = true;
            }
        }
        else
        {
            if (_evidenceController != null)
            {
                if (_evidenceController.IsHover)
                {
                    _evidenceController.IsHover = false;
                }

                _evidenceController = null;
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
        CurrentStation = PlayerLocation.FilingCabinet;
    }

}

public enum PlayerLocation
{
    Desk,
    EvidenceBoard,
    FilingCabinet
}
