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

    [SerializeField] private PlayerLocation CurrentStation;
    [SerializeField] private Camera SceneCamera;
    private Vector3 lastPosition;

    [SerializeField] private float CastDistance;

    [SerializeField] private LayerMask LevelMask, EvidenceMask, CabinetMask;

    private Vector3 mousePosition;

    private EvidenceController _evidenceController;

    [SerializeField] private YarnController _yarnController;

    [SerializeField] private PlayerController _playerController;

    [SerializeField] private GameObject _cabinetController;

    private ClickControls _clickInputs;

    private void Awake()
    {
        /*_playerInput = GetComponent<PlayerInput>();

        _playerInput.currentActionMap.Enable();

        leftClickAction = _playerInput.currentActionMap.FindAction("LeftClick");
        rightClickAction = _playerInput.currentActionMap.FindAction("RightClick");*/

        _clickInputs = new ClickControls();
        _clickInputs.DefaultControls.Enable();
        _clickInputs.DefaultControls.RightClick.started += rightClickAction_started;

       // leftClickAction.started += leftClickAction_started;
        //leftClickAction.canceled += leftClickAction_canceled;

        //rightClickAction.started += rightClickAction_started;
    }

    private void leftClickAction_started(InputAction.CallbackContext obj)
    {
        isSelecting = true;

        if (_evidenceController != null )
        {
            _evidenceController.IsHeld = true;
        }
    }

    private void leftClickAction_canceled(InputAction.CallbackContext obj)
    {
        isSelecting = false;

        if (_evidenceController != null)
        {
            _evidenceController.OnPlace();
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
                    _yarnController.CheckLineStatus(hitObject.transform.position, hitObject.EvidenceData);

                    _yarnController = null;
                }
                else
                {
                    _yarnController = hit.collider.gameObject.GetComponent<YarnController>();

                    _yarnController.IsConnecting = true;

                    _yarnController.CheckLineStatus(hitObject.transform.position, hitObject.EvidenceData);
                }
            }
        }

        //Quinn wrote this. For opening/closing the filing cabinet
        //Debug.Log("It ran the thing");
        RaycastHit hitCabinet;
        if (Physics.Raycast(SceneCamera.ScreenPointToRay(mousePosition), out hitCabinet, CastDistance, CabinetMask))
        {
            Debug.Log("Hit the Cabinet");
            //_cabinetController.GetComponent<CabinetController>().GetOpenClose();
            hitCabinet.collider.gameObject.GetComponent<CabinetController>().GetOpenClose();
        }
        else
        {
            Debug.DrawLine(SceneCamera.transform.position, SceneCamera.ScreenPointToRay(mousePosition).direction* CastDistance,Color.red,5);
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
