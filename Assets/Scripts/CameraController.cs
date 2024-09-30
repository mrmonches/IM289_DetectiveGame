using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

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
    //used to prevent the player from moving while in the typewriter
    private bool _canMove = true;
    [SerializeField] private Camera mainCamera;

    // Start is called before the first frame update
    void Awake()
    {
        
        CameraInputs.currentActionMap.Enable();
        cameraLeft = CameraInputs.currentActionMap.FindAction("Left");
        cameraRight = CameraInputs.currentActionMap.FindAction("Right");

        cameraLeft.started += CameraLeft_started;
        cameraRight.started += CameraRight_started;


        LeftCamera.gameObject.SetActive(false);
        RightCamera.gameObject.SetActive(false);
        MiddleCamera.gameObject.SetActive(true);

        _playerController.GetComponent<PlayerController>().StationSetDesk();
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
    private void OnDestroy()
    {
        cameraRight.started -= CameraRight_started;
        cameraLeft.started -= CameraLeft_started;
        
    }
}
