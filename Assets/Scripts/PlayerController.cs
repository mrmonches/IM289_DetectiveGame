using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput _playerInput;

    private InputAction selectAction;

    private bool isSelecting;

    [SerializeField] private PlayerLocation CurrentStation;

    [SerializeField] private Camera SceneCamera;
    private Vector3 lastPosition;

    [SerializeField] private float CastDistance;

    [SerializeField] private LayerMask LevelMask, EvidenceMask;

    private Vector3 mousePosition;

    private EvidenceController _evidenceController;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        _playerInput.currentActionMap.Enable();

        selectAction = _playerInput.currentActionMap.FindAction("Select");

        selectAction.started += selectAction_started;
        selectAction.canceled += selectAction_canceled;
    }

    private void selectAction_started(InputAction.CallbackContext obj)
    {
        isSelecting = true;

        if (_evidenceController != null )
        {
            _evidenceController.IsHeld = true;
        }
    }

    private void selectAction_canceled(InputAction.CallbackContext obj)
    {
        isSelecting = false;

        if (_evidenceController != null)
        {
            _evidenceController.IsHeld = false;

            _evidenceController.RecordPlacedPos();
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
