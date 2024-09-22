// Nolan wrote this script
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EvidenceID", menuName = "EvidenceID")]
public class EvidenceData : ScriptableObject
{
    [SerializeField] private EvidenceID _evidenceID;
    [SerializeField] private EvidenceType _evidenceType;
    [SerializeField] private List<EvidenceID> correctConnections = new List<EvidenceID>();

    [SerializeField] private bool TypewriterEvidence;

    public EvidenceID EvidenceID { get => _evidenceID; set => _evidenceID = value; }
    public EvidenceType EvidenceType { get => _evidenceType; set => _evidenceType = value; }
    public bool TypewriterEvidence1 { get => TypewriterEvidence; set => TypewriterEvidence = value; }

    public bool CheckCorrectConnection(EvidenceID evidenceID)
    {
        for (int i = 0; i < correctConnections.Count; i++)
        {
            if (correctConnections[i] == evidenceID)
            {
                return true;
            }
        }

        return false;
    }
}

public enum EvidenceID
{
    // First letter which case, A-C
    // First set of numbers, which piece of evidence
    // Second set of numbers, ID for component of evidence
    Default,
    A01_01,
    A01_02,
    A01_03,
    A01_04,
    A01_05
}

public enum EvidenceType
{
    Picture, 
    Blueprint,
    MurderWeapon,
    WitnessStatement,
    Document
}