using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SystemManager : MonoBehaviour
{
    //Created by Quinn
    private PlayerInput _playerInput;

    private InputAction _quitAction;
    private InputAction _restartAction;

    private DebugControls _debugInputs;

    private bool paperopen;
    private TypeWriterController _typeWriterController;
    private DocumentTurnPage _documentTurnPage;
    private string openDoc;
    private GameObject openEvidence;
    [SerializeField] private GameObject pausemenu;
    private CameraController _cameraController;
    private bool paused = false;
    //[SerializeField] private 
    // Start is called before the first frame update
    void Start()
    {
        _debugInputs = new DebugControls();
        _debugInputs.Debug.Enable();
       // _debugInputs.Debug.Quit.started += Quit_started;
        _debugInputs.Debug.Restart.started += Restart_started;
        _documentTurnPage = FindObjectOfType<DocumentTurnPage>();
        _typeWriterController = FindObjectOfType<TypeWriterController>();
        _cameraController = FindObjectOfType<CameraController>();
        pausemenu.gameObject.SetActive(false);
    }

    private void Restart_started(InputAction.CallbackContext obj)
    {
        //Currently reloads the scene you're in. This is known to cause errors in the gameplay scene. They don't appear to 
        //be fatal. 
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

   /* private void Quit_started(InputAction.CallbackContext obj)
    {
        Debug.Log("test 2");
        if(paperopen==true)
        {
            if(openDoc=="TypeWriter")
            {
                _typeWriterController.BackToDesk();
            }
            else if (openDoc=="Doc")
            {
                GameObject.Find("Player").GetComponent<PlayerController>().InItemViewer = false;
                Destroy(openEvidence);
            }


            updatePaperOpen(false, "null");
            
        }
        if(paused==true)
        {
            pausemenu.gameObject.SetActive(false);
            _cameraController.updatePause(false);
            paused = false;
        }
        else
        {
            pausemenu.gameObject.SetActive(true);
            _cameraController.updatePause(true);
            paused = true;

        }
    }*/
    public void updatePaperOpen(bool input, string inputS)
    {
        if(input==true)
        {
            paperopen = true;
        }
        else
        {
            paperopen = false;
        }
        openDoc = inputS;

    }
    public void getOpenDoc(GameObject input)
    {
        openEvidence = input;
    }
    public void unPause()
    {
        pausemenu.gameObject.SetActive(false);
        _cameraController.updatePause(false);
    }


    
}
