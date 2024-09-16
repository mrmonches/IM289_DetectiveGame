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
    //SceneCamera was renamed to CurrentCamera
    [SerializeField] private Camera CurrentCamera;
    [SerializeField] private Camera EvidenceCam;
    [SerializeField] private Camera FilingCam;
    [SerializeField] private Camera DeskCam;
    private Vector3 lastPosition;

    [SerializeField] private float CastDistance;

    [SerializeField] private LayerMask LevelMask, EvidenceMask, CabinetMask;

    private Vector3 mousePosition;

    private EvidenceController _evidenceController;

    [SerializeField] private YarnController _yarnController;

    [SerializeField] private PlayerController _playerController;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        _playerInput.currentActionMap.Enable();

        leftClickAction = _playerInput.currentActionMap.FindAction("LeftClick");
        rightClickAction = _playerInput.currentActionMap.FindAction("RightClick");

        leftClickAction.started += leftClickAction_started;
        leftClickAction.canceled += leftClickAction_canceled;

        rightClickAction.started += rightClickAction_started;
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
        RaycastHit hit;

        if (Physics.Raycast(CurrentCamera.ScreenPointToRay(mousePosition), out hit, CastDistance, EvidenceMask))
        {
            if (_yarnController != null && _yarnController.IsConnecting)
            {
                _yarnController.GiveLinePosition(hit.collider.GetComponent<EvidenceController>().ChildTransform.position);

                print(hit.collider.GetComponentInChildren<Transform>().position);
            }
            else
            {
                _yarnController = hit.collider.gameObject.GetComponent<YarnController>();

                _yarnController.GiveLinePosition(hit.collider.GetComponent<EvidenceController>().ChildTransform.position);

                print(hit.collider.GetComponentInChildren<Transform>().position);

                _yarnController.IsConnecting = true;
            }
        }
        else
        {

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
        mousePosition.z = CurrentCamera.nearClipPlane;

        RaycastHit hit; 
        if (Physics.Raycast(CurrentCamera.ScreenPointToRay(mousePosition), out hit, CastDistance, LevelMask))
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

        if (Physics.Raycast(CurrentCamera.ScreenPointToRay(mousePosition), out hit, CastDistance, EvidenceMask))
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
}

public enum PlayerLocation
{
    Desk,
    EvidenceBoard,
    FilingCabinet
}
