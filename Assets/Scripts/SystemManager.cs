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
    //[SerializeField] private 
    // Start is called before the first frame update
    void Start()
    {
        _debugInputs = new DebugControls();
        _debugInputs.Debug.Enable();
        _debugInputs.Debug.Quit.started += Quit_started;
        _debugInputs.Debug.Restart.started += Restart_started;
    }

    private void Restart_started(InputAction.CallbackContext obj)
    {
        //Currently reloads the scene you're in. This is known to cause errors in the gameplay scene. They don't appear to 
        //be fatal. 
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    private void Quit_started(InputAction.CallbackContext obj)
    {
        Application.Quit();
    }

    
}
