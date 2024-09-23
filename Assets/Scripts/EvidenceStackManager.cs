using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceStackManager : MonoBehaviour
{
    [SerializeField] private List<EvidenceData> StackList;

    public void AddToStack(EvidenceData evidenceData)
    {
        StackList.Add(evidenceData);
    }

    public void GivePlayerEvidence()
    {

    }
}
