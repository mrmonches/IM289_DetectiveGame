using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceStackManager : MonoBehaviour
{
    [SerializeField] private List<EvidenceData> StackList;

    private PlayerController _playerController;

    [SerializeField] private GameObject EvidenceCardObject;

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
        GameObject card = Instantiate(EvidenceCardObject, _playerController.GetSelectedPosition(), Quaternion.identity);

        _playerController.EvidenceController = card.GetComponent<EvidenceController>();
    }
}
