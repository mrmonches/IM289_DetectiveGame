using Cinemachine;
using System.Collections;
using System.Collections.Generic;
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
    //1= left camera 2=middle 3=right
    private int activecamera = 2;
    // Start is called before the first frame update
    void Start()
    {
        CameraInputs.currentActionMap.Enable();
        cameraLeft = CameraInputs.currentActionMap.FindAction("Left");
        cameraRight = CameraInputs.currentActionMap.FindAction("Right");

        cameraLeft.started += CameraLeft_started;
        cameraRight.started += CameraRight_started;
        LeftCamera.gameObject.SetActive(false);
        RightCamera.gameObject.SetActive(false);
        MiddleCamera.gameObject.SetActive(true);
    }

    
    private void CameraRight_started(InputAction.CallbackContext obj)
    {
        //moves camera to the one on the right and keeps track of the active camera
        if (activecamera == 1)
        {
            LeftCamera.gameObject.SetActive(false);
            MiddleCamera.gameObject.SetActive(true);
            RightCamera.gameObject.SetActive(false);
            activecamera = 2;
        }
        else if(activecamera==2)
        {
            LeftCamera.gameObject.SetActive(false);
            MiddleCamera.gameObject.SetActive(false);
            RightCamera.gameObject.SetActive(true);
            activecamera = 3;
        }
    }

    private void CameraLeft_started(InputAction.CallbackContext obj)
    {
        //same as above but for the right
        if(activecamera==2)
        {
            RightCamera.gameObject.SetActive(false);
            MiddleCamera.gameObject.SetActive(false);
            LeftCamera.gameObject.SetActive(true);
            activecamera = 1;
        }
        else if(activecamera==3)
        {
            RightCamera.gameObject.SetActive(false);
            MiddleCamera.gameObject.SetActive(true);
            LeftCamera.gameObject.SetActive(false);
            activecamera = 2;
        }
    }
}
