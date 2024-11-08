using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileController : MonoBehaviour
{
    //Written by Quinn
    //[SerializeField] private List<EvidenceData> heldDataFile;

    private PlayerController _playerController;

    [SerializeField] private EvidenceStackManager _evidenceStackManager;

    private AudioSource _audioSource;

    [SerializeField] private AudioClip WriteClip;

    private void OnEnable()
    {
        _playerController = FindObjectOfType<PlayerController>();

        _evidenceStackManager = FindObjectOfType<EvidenceStackManager>();

        _audioSource = GetComponent<AudioSource>();
    }

    public void CloseFile()
    {
        _playerController.InItemViewer = false;

        Destroy(gameObject);
    }

    public void SendToBoard(EvidenceData evidenceData)
    {
        if (_evidenceStackManager.GetStackCount() < 5)
        {
            _audioSource.PlayOneShot(WriteClip);

            _evidenceStackManager.AddToStack(evidenceData);
        }
        else
        {
            print("Too many cards");
        }
    }
}
