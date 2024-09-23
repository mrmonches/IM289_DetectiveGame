using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceStackManager : MonoBehaviour
{
    [SerializeField] private List<EvidenceData> StackList;

    private PlayerController _playerController;

    [SerializeField] private GameObject EvidenceCardObject;

    [SerializeField] private Transform EvidenceRotation;

    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    public void AddToStack(EvidenceData evidenceData)
    {
        StackList.Add(evidenceData);
    }

    public void GivePlayerEvidence()
    {
        if (StackList.Count > 0)
        {
            GameObject card = Instantiate(EvidenceCardObject, _playerController.GetSelectedPosition(), EvidenceRotation.rotation);

            EvidenceController evidenceController = card.GetComponent<EvidenceController>();

            _playerController.EvidenceController = evidenceController;

            evidenceController.IsHeld = true;

            evidenceController.GiveEvidenceData(StackList[0]);

            StackList.RemoveAt(0);
        }
    }
}
