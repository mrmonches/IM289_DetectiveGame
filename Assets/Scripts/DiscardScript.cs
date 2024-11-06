using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardScript : MonoBehaviour
{
    private EvidenceBoardManager _boardManager;

    private void Awake()
    {
        _boardManager = FindObjectOfType<EvidenceBoardManager>();
    }

    public void DiscardCard(EvidenceID id, GameObject card)
    {
        _boardManager.RemoveConnectionFromList(id);

        Destroy(card);
    }
}
