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

    [SerializeField] private Transform MinPos, MaxPos;

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
        posDifference = Vector3.Distance(MinPos.transform.position, MaxPos.transform.position) / CardList.Count;

        print(posDifference);

        //rotDifference = Vector3.Angle(MinPos.transform.rotation, MaxPos.transform.rotation) / CardList.Count;

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

                print("negative " + (posDifference * negativeIncrement));

                negativeIncrement++;
            }
            else if ((float)i == count / 2.0f)
            {
                CardList[i].transform.position = CardSpawnPos.transform.position;
                //print("neutral");
            }
            else if ((float)i > count / 2.0f)
            {
                CardList[i].transform.position = 
                    new Vector3(CardSpawnPos.transform.position.x - (posDifference * (positiveIncrement)),
                        CardSpawnPos.transform.position.y, 
                        CardSpawnPos.transform.position.z);

                print("positive " + (posDifference * positiveIncrement));

                positiveIncrement--;
                //print("positive");
            }

            //CardList[i].transform.Rotate(Vector3.forward, rotDifference);
        }
    }
    // Connection of evidence cards needs to be childed to evidence board camera
    // Connection need to be added and accessible whenever the camera is active at the evidence board
    // Whenever the player hovers the mouse cursor over an evidence card, it should move upwards
    // Whenever the player removes the evidence card from the connection, it will then be "on" the board
    // If the player places the card where it cannot be placed, return card to collection


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
