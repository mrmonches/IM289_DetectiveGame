using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileController : MonoBehaviour
{
    //Written by Quinn
    public EvidenceData heldDataFile;

    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    public void CloseFile()
    {
        _playerController.InItemViewer = false;

        Destroy(gameObject);
    }
}
