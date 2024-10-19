using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileController : MonoBehaviour
{
    //Written by Quinn
    //[SerializeField] private List<EvidenceData> heldDataFile;

    private PlayerController _playerController;

    [SerializeField] private EvidenceStackManager _evidenceStackManager;

    private void OnEnable()
    {
        _playerController = FindObjectOfType<PlayerController>();

        _evidenceStackManager = FindObjectOfType<EvidenceStackManager>();
    }

    public void CloseFile()
    {
        _playerController.InItemViewer = false;

        Destroy(gameObject);
    }

    public void SendToBoard(EvidenceData evidenceData)
    {
        _evidenceStackManager.AddToStack(evidenceData);
    }
}
