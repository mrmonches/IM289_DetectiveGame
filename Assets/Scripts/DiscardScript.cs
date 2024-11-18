using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardScript : MonoBehaviour
{
    private EvidenceBoardManager _boardManager;

    private EvidenceStackManager _stackManager;

    private void Awake()
    {
        _boardManager = FindObjectOfType<EvidenceBoardManager>();
        _stackManager = FindObjectOfType<EvidenceStackManager>();
    }

    public void DiscardCard(EvidenceID id, GameObject card)
    {
        _boardManager.RemoveConnectionFromList(id);

        if (_stackManager.CheckInStack(card))
            _stackManager.RemoveFromStack(card.GetComponent<EvidenceController>().EvidenceData, card);

        Destroy(card);
    }
}
