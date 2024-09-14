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

    [SerializeField] private LayerMask LevelMask;

    private Vector3 mousePosition;

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
    }

    private void selectAction_canceled(InputAction.CallbackContext obj)
    {
        isSelecting = false;
    }

    private void OnMouse(InputValue mousePos)
    {
        mousePosition = mousePos.Get<Vector2>();
    }

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

    private void FixedUpdate()
    {
        switch (CurrentStation)
        {
            case PlayerLocation.Desk:
                break;
            case PlayerLocation.EvidenceBoard:
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
