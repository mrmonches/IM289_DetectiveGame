using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EvidenceStackManager : MonoBehaviour
{
    [SerializeField] private List<EvidenceData> StackList;
    [SerializeField] private List<GameObject> CardList;

    private PlayerController _playerController;

    [SerializeField] private GameObject EvidenceCardObject;

    [SerializeField] private Transform EvidenceRotation;

    [SerializeField] private GameObject CardSpawnPos;

    [SerializeField] private List<GameObject> SpecialCaseItems;

    [SerializeField] private float CardDistance, CardRotation;

    [SerializeField] private GameObject[] OddCardPos = new GameObject[5];
    [SerializeField] private GameObject[] EvenCardPos = new GameObject[4];

    //private int CardMax = 5;

    private float posDifference, rotDifference;

    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    public int GetStackCount()
    {
        return StackList.Count;
    }

    public void AddToStack(EvidenceData evidenceData, FileController fileController)
    {
        StackList.Add(evidenceData);

        var newCard = Instantiate(FindEvidenceItem(evidenceData.EvidenceID), CardSpawnPos.transform.position, EvidenceRotation.rotation);

        EvidenceController evidenceController = newCard.GetComponent<EvidenceController>();

        evidenceController.GiveEvidenceData(evidenceData, fileController);

        newCard.transform.parent = CardSpawnPos.transform;

        CardList.Add(newCard);

        UpdateStackOrder();
    }

    public void RemoveFromStack(EvidenceData evidenceData, GameObject evidenceObject)
    {
        StackList.Remove(evidenceData);
        CardList.Remove(evidenceObject);

        UpdateStackOrder();
    }

    public bool CheckInStack(GameObject card)
    {
        foreach (var item in CardList)
        {
            if (item == card) 
                return true;
        }

        return false;
    }

    /// <summary>
    /// This is a very manual function that works, just never had enough time to make it less manual
    /// </summary>
    public void UpdateStackOrder()
    {
        switch (StackList.Count)
        {
            case 1:
                CardList[0].transform.position = OddCardPos[2].transform.position;
                break;
            case 2:
                CardList[0].transform.position = EvenCardPos[1].transform.position;
                CardList[1].transform.position = EvenCardPos[2].transform.position;
                break;
            case 3:
                CardList[0].transform.position = OddCardPos[1].transform.position;
                CardList[1].transform.position = OddCardPos[2].transform.position;
                CardList[2].transform.position = OddCardPos[3].transform.position;
                break;
            case 4:
                int i = 0;
                foreach (var card in CardList) 
                {
                    card.transform.position = EvenCardPos[i].transform.position;
                    i++;
                }
                break;
            case 5:
                int x = 0;
                foreach (var card in CardList)
                {
                    card.transform.position = OddCardPos[x].transform.position;
                    x++;
                }
                break;
            default:
                print("No cards");
                break;
        }
    }

    /// <summary>
    /// Part of UpdateStackOrder that needs to be corrected
    /// </summary>
    /// <param name="offset"></param>
    /// <param name="isOdd"></param>
    /// <returns></returns>
    private GameObject[] CalculateTempList(int offset, bool isOdd)
    {
        GameObject[] tempList;

        if (isOdd)
        {
            tempList = new GameObject[5];
        }
        else
        {
            tempList = new GameObject[4];
        }

        int x = 0;

        // Assigns values before center cards as null
        for (int i = 0; i < offset; i++)
        {
            tempList[i] = null;

            x = i;
        }

        for (int i = x; i < StackList.Count; i++)
        {
            tempList[i] = CardList[i];
        }

        return tempList;
    }

    // Not being used
    //public void GivePlayerEvidence()
    //{
    //    if (StackList.Count > 0)
    //    {
    //        GameObject card;

    //        if (StackList[0].EvidenceType == EvidenceType.Document)
    //        {
    //            card = Instantiate(EvidenceCardObject, _playerController.GetSelectedPosition(), EvidenceRotation.rotation);
    //        }
    //        else
    //        {
    //            card = Instantiate(FindEvidenceItem(StackList[0].EvidenceID), _playerController.GetSelectedPosition(), EvidenceRotation.rotation);
    //        }

    //        EvidenceController evidenceController = card.GetComponent<EvidenceController>();

    //        _playerController.EvidenceController = evidenceController;

    //        evidenceController.IsHeld = true;

    //        evidenceController.GiveEvidenceData(StackList[0], );

    //        StackList.RemoveAt(0);
    //    }
    //}

    /// <summary>
    /// Function meant to behave differently whenever a non-document is grabbed
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public GameObject FindEvidenceItem(EvidenceID id)
    {
        for (int i = 0; i < SpecialCaseItems.Count; i++)
        {
            if (SpecialCaseItems[i].GetComponent<EvidenceController>().EvidenceData.EvidenceID == id)
            {
                return SpecialCaseItems[i];
            }
        }

        return EvidenceCardObject;
    }
}
