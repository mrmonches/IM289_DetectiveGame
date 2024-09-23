using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CabinetController : MonoBehaviour
{
    //Quinn wrote this
    private bool _isOpened = false;
    private Vector3 _closedVector3;
    private Vector3 _openedVector3;
    [SerializeField] private float pullOutDistance;
    [SerializeField] private GameObject folders;

    public bool IsOpened { get => _isOpened; set => _isOpened = value; }

    private void Start()
    {
        _closedVector3 = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        _openedVector3 = new Vector3(transform.position.x, transform.position.y, transform.position.z + pullOutDistance);
    }
    public void GetOpenClose()
    {
        OpenClose();
    }

    private void OpenClose()
    {
        if(IsOpened == true)
        {
            CloseCabinet();
        }
        else
        {
            OpenCabinet();
        }
    }
    private void OpenCabinet()
    {
        transform.position = _openedVector3;
        IsOpened = true;
        folders.SetActive(true);
    }

    private void CloseCabinet()
    {
        transform.position = _closedVector3;
        IsOpened = false;
        folders.SetActive(false);
    }

    public void GetOpen()
    {
        OpenCabinet();
    }

    public void GetClose()
    {
        CloseCabinet();
    }
}
