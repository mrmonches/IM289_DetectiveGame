using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CabinetController : MonoBehaviour
{
    private bool _isOpened = false;
    private Vector3 _closedVector3;
    private Vector3 _openedVector3;
    [SerializeField] private float pullOutDistance;

    private void Start()
    {
        _closedVector3 = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        //_openedTransform = transform;
        _openedVector3 = new Vector3(transform.position.x, transform.position.y, transform.position.z + pullOutDistance);
    }
    public void GetOpenClose()
    {
        OpenClose();
    }

    void OpenClose()
    {
        if(_isOpened == true)
        {
            CloseCabinet();
        }
        else
        {
            OpenCabinet();
        }
    }
   void OpenCabinet()
    {
        transform.position = _openedVector3;
        _isOpened = true;
    }

    void CloseCabinet()
    {
        transform.position = _closedVector3;
        _isOpened = false;
    }
}
