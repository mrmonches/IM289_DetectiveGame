using System.Collections;
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

    private float posDifference, rotDifference;

    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    public void AddToStack(EvidenceData evidenceData)
    {
        StackList.Add(evidenceData);

        var newCard = Instantiate(EvidenceCardObject, CardSpawnPos.transform.position, EvidenceRotation.rotation);

        newCard.transform.parent = CardSpawnPos.transform;

        CardList.Add(newCard);

        UpdateStackOrder();
    }


    private void UpdateStackOrder()
    {
        posDifference = CardDistance / CardList.Count;

        int count = CardList.Count - 1;

        int negativeIncrement = 1;
        int positiveIncrement = CardList.Count / 2;
        

        for (int i = 0; i < CardList.Count; i++)
        {
            if ((float)i < count / 2.0f)
            {
                CardList[i].transform.position = 
                    new Vector3(CardSpawnPos.transform.position.x + (posDifference * (negativeIncrement)),
                        CardSpawnPos.transform.position.y, 
                        CardSpawnPos.transform.position.z);

                negativeIncrement++;
            }
            else if ((float)i == count / 2.0f)
            {
                CardList[i].transform.position = CardSpawnPos.transform.position;
            }
            else if ((float)i > count / 2.0f)
            {
                CardList[i].transform.position = 
                    new Vector3(CardSpawnPos.transform.position.x - (posDifference * (positiveIncrement)),
                        CardSpawnPos.transform.position.y, 
                        CardSpawnPos.transform.position.z);

                positiveIncrement--;
            }

            //CardList[i].transform.Rotate(Vector3.forward, rotDifference);
        }
    }

    public void GivePlayerEvidence()
    {
        if (StackList.Count > 0)
        {
            GameObject card;

            if (StackList[0].EvidenceType == EvidenceType.Document)
            {
                card = Instantiate(EvidenceCardObject, _playerController.GetSelectedPosition(), EvidenceRotation.rotation);
            }
            else
            {
                card = Instantiate(FindEvidenceItem(StackList[0].EvidenceID), _playerController.GetSelectedPosition(), EvidenceRotation.rotation);
            }

            EvidenceController evidenceController = card.GetComponent<EvidenceController>();

            _playerController.EvidenceController = evidenceController;

            evidenceController.IsHeld = true;

            evidenceController.GiveEvidenceData(StackList[0]);

            StackList.RemoveAt(0);
        }
    }

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
