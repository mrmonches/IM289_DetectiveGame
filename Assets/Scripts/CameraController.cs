using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private PlayerInput CameraInputs;
    private InputAction cameraLeft;
    private InputAction cameraRight;
    [SerializeField] private CinemachineVirtualCamera LeftCamera;
    [SerializeField] private CinemachineVirtualCamera MiddleCamera;
    [SerializeField] private CinemachineVirtualCamera RightCamera;
    //1= left camera MurderBoard | 2= middle Desk | 3= right Filing Cabinet
    private int activecamera = 2;
    [SerializeField] private GameObject _playerController;
    [SerializeField] private GameObject _cabinetController;

    private CinemachineBrain _cinemachineBrain;

    //used to prevent the player from moving while in the typewriter
    private bool _canMove = true;
    [SerializeField] private Camera mainCamera;

    // Allows camera to move freely when inside evidence board
    [SerializeField, Tooltip ("How fast the invisible box is moving around the scene")] private float BoxSpeed;
    [SerializeField] private float ScrollSpeed;
    [SerializeField, Tooltip("How fast the camera moves on player input")] private float SlerpSpeed;
    [SerializeField] private Rigidbody _boxRB;
    [SerializeField] private GameObject BoardBox;

    // Start is called before the first frame update
    void Awake()
    {
        CameraInputs.currentActionMap.Enable();
        cameraLeft = CameraInputs.currentActionMap.FindAction("StationLeft");
        cameraRight = CameraInputs.currentActionMap.FindAction("StationRight");

        cameraLeft.started += CameraLeft_started;
        cameraRight.started += CameraRight_started;

        LeftCamera.gameObject.SetActive(false);
        RightCamera.gameObject.SetActive(false);
        MiddleCamera.gameObject.SetActive(true);

        _playerController.GetComponent<PlayerController>().StationSetDesk();

        _cinemachineBrain = GetComponent<CinemachineBrain>();
    }

    
    private void CameraRight_started(InputAction.CallbackContext obj)
    {
        //moves camera to the one on the right and keeps track of the active camera
        if (activecamera == 1 && _canMove == true)
        {
            LeftCamera.gameObject.SetActive(false);
            MiddleCamera.gameObject.SetActive(true);
            RightCamera.gameObject.SetActive(false);
            activecamera = 2;

            _playerController.GetComponent<PlayerController>().StationSetDesk();
        }
        else if(activecamera==2 && _canMove == true)
        {
            LeftCamera.gameObject.SetActive(false);
            MiddleCamera.gameObject.SetActive(false);
            RightCamera.gameObject.SetActive(true);
            activecamera = 3;

            _playerController.GetComponent<PlayerController>().StationSetCabinet();
            _cabinetController.GetComponent<CabinetController>().GetOpen();
        }
        else if(activecamera == 3 && _canMove == true)
        {
            LeftCamera.gameObject.SetActive(true);
            MiddleCamera.gameObject.SetActive(false);
            RightCamera.gameObject.SetActive(false);
            activecamera = 1;
            _playerController.GetComponent<PlayerController>().StationSetBoard();
        }
    }

    private void CameraLeft_started(InputAction.CallbackContext obj)
    {
        //same as above but for the right
        if(activecamera == 2 && _canMove == true)
        {
            RightCamera.gameObject.SetActive(false);
            MiddleCamera.gameObject.SetActive(false);
            LeftCamera.gameObject.SetActive(true);
            activecamera = 1;

            _playerController.GetComponent<PlayerController>().StationSetBoard();
        }
        else if(activecamera == 3 && _canMove == true)
        {
            RightCamera.gameObject.SetActive(false);
            MiddleCamera.gameObject.SetActive(true);
            LeftCamera.gameObject.SetActive(false);
            activecamera = 2;

            _playerController.GetComponent<PlayerController>().StationSetDesk();
            _cabinetController.GetComponent<CabinetController>().GetClose();
        }
        else if(activecamera == 1 && _canMove == true)
        {
            LeftCamera.gameObject.SetActive(false);
            MiddleCamera.gameObject.SetActive(false);
            RightCamera.gameObject.SetActive(true);
            activecamera = 3;
            _playerController.GetComponent<PlayerController>().StationSetCabinet();
        }
    }

    /// <summary>
    /// This allows the player to move the camera while at the evidence board
    /// Looks nice and smooth :)
    /// </summary>
    public void MoveBoardCamera(Vector2 moveValue)
    {
        if (!_cinemachineBrain.IsBlending)
        {
            _boxRB.velocity = new Vector3(-moveValue.x * BoxSpeed, moveValue.y * BoxSpeed, _boxRB.velocity.z);
        }
    }

    /// <summary>
    /// Overload of previous method so that player can zoom in
    /// </summary>
    public void ZoomBoardCamera(float moveValue)
    {
        if (!_cinemachineBrain.IsBlending)
        {
            _boxRB.velocity = new Vector3(_boxRB.velocity.x, _boxRB.velocity.y, moveValue * ScrollSpeed);
        }
    }

    /// <summary>
    /// These are used to prevent the player from moving while in the typewriter. 
    /// </summary>
    public void getCanMove()
    {
        CanMove();
    }

    public void getCannotMove()
    {
        CannotMove();
    }

    void CanMove()
    {
        _canMove = true;
    }

    void CannotMove()
    {
        _canMove= false;
    }

    /// <summary>
    /// DO NOT CHANGE FROM FIXED UPDATE
    /// The camera transitions ONLY WORK when this function is called on FixedUpdate
    /// Thanks - Nolan
    /// </summary>
    private void FixedUpdate()
    {
        if (!_cinemachineBrain.IsBlending && activecamera == 1)
        {
            LeftCamera.transform.position = Vector3.Slerp(transform.position, BoardBox.transform.position, SlerpSpeed * Time.deltaTime);
        }
    }

    private void OnDestroy()
    {
        cameraRight.started -= CameraRight_started;
        cameraLeft.started -= CameraLeft_started;
    }
}
