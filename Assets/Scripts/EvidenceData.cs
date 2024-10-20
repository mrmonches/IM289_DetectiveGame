// Nolan wrote this script
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EvidenceID", menuName = "EvidenceID")]
public class EvidenceData : ScriptableObject
{
    [SerializeField] private EvidenceID _evidenceID;
    [SerializeField] private EvidenceType _evidenceType;
    [SerializeField] private List<EvidenceID> correctConnections = new List<EvidenceID>();

    [SerializeField, Tooltip("This should only be filled out if the evidence is part of a document")]
    private string CardInformation;

    [SerializeField] private bool TypewriterEvidence;

    public EvidenceID EvidenceID { get => _evidenceID; set => _evidenceID = value; }
    public EvidenceType EvidenceType { get => _evidenceType; set => _evidenceType = value; }
    public bool TypewriterEvidence1 { get => TypewriterEvidence; set => TypewriterEvidence = value; }
    public string GetCardInformation { get => CardInformation; private set => CardInformation = value; }

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
    //*Quinn here. I'm not sure the the line above ^^ works
    //*Some pieces of evidence are on multiple documents. Maybe I'm not reading the documentation right,
    //* it's a little confusing
    // Second set of numbers, ID for component of evidence
    // If ID ends in 00, means evidence does not have any other components, sent to board 'as is'
    Default,
    A01_01, //Darling King
    A01_02, //Victims wife
    A01_03, //Steven Knight
    A01_04, //Victims close friend
    A01_05, //Queen Bee
    A01_06, //Guoco Piano
    A01_07, //Rookie
    A01_08, //Map of city street
    A01_09, //Large frame revolver
    A01_10, //Victim was intoxicated
    A01_11, // 0.357 magnun cartridge
    A01_12, //Jimmy King
    A01_13, //
    A01_14, //Bloodloss
    A01_15, //Watch
    A01_16, //Wallets Contents
    A02_00,
    A02_01, //Alleyway
    A02_02, //Picture of King's Body
    A02_03, //Victim was executed
    A03_00,
    A03_01, //1471 Diamond St. (Personal Residence)
    A03_02, //Personal Chest
    A04_01,
    A04_02, //Rat
    A04_03, //Childhood Friend
    A04_04, //Made a move
    A04_05, //The Red Stiletto

}

public enum EvidenceType
{
    //* Quinn here. This needs to be reworked. Several of these are functionally the same and don't need to be seperated. 
    Picture, 
    Blueprint,
    MurderWeapon,
    WitnessStatement,
    Document
}