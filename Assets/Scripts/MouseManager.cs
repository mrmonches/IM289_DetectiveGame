using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseManager : MonoBehaviour
{
    [SerializeField] private Camera SceneCamera;

    [SerializeField] private float CastDistance;

    [SerializeField] private LayerMask CastLayers;

    private PlayerController _playerController;

    [SerializeField] private Texture2D DefaultCursor, HoverCursor;

    [SerializeField] private Vector2 HoverOffset;

    [SerializeField] private PhoneManager _phoneManager;

    [SerializeField] private GraphicRaycaster _graphicRaycaster;

    private EventSystem _eventSystem;

    [SerializeField] private bool inTitleScene;

    private void OnEnable()
    {
        if (!inTitleScene)
        {
            _playerController = FindObjectOfType<PlayerController>();

            _eventSystem = FindObjectOfType<EventSystem>();
        }
        else
        {
            SetDefaultCursor();
        }
    }

    private void CheckMouseStatus()
    {
        RaycastHit hit;

        if (Physics.Raycast(SceneCamera.ScreenPointToRay(_playerController.GetMousePosition), out hit, CastDistance, CastLayers)
            && _playerController.GetPlayerStatus()) 
        {
            if (hit.collider.gameObject.GetComponent<PhoneManager>() != null && _phoneManager.IsRinging)
            {
                SetHoverCursor();
            }
            else if (hit.collider.gameObject.GetComponent<PhoneManager>() != null && !_phoneManager.IsRinging)
            {
                SetDefaultCursor();
            }
            else
            {
                SetHoverCursor();
            }
        }
        else
        {
            SetDefaultCursor();
        }

        //if (!_playerController.GetPlayerStatus())
        //{
        //    PointerEventData eventData = new PointerEventData(_eventSystem);

        //    List<RaycastResult> results = new List<RaycastResult>();

        //    _graphicRaycaster.Raycast(eventData, results);

        //    foreach (RaycastResult result in results)
        //    {
        //        if (result.gameObject.GetComponent<FileController>() != null)
        //        {
        //            SetHoverCursor();
        //        }
        //    }
        //}
    }

    private void SetDefaultCursor()
    {
        Cursor.SetCursor(DefaultCursor, Vector2.zero, CursorMode.ForceSoftware);
    }
    
    private void SetHoverCursor()
    {
        Cursor.SetCursor(HoverCursor, HoverOffset, CursorMode.ForceSoftware);
    }

    private void FixedUpdate()
    {
        if (!inTitleScene)
        {
            CheckMouseStatus();
        }
    }
}
