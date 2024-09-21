using System.Collections.Generic;
using UnityEngine;

//By Nolan

public class EvidenceBoardManager : MonoBehaviour
{
    [SerializeField] private float EvidencePlacePos;
    [SerializeField] private List<ConnectionData> connections = new List<ConnectionData>();

    public float EvidencePlacePos1 { get => EvidencePlacePos; private set => EvidencePlacePos = value; }
    public List<ConnectionData> Connections { get => connections; set => connections = value; }


    public void CheckConnectionList(EvidenceID firstID, EvidenceID secondID, YarnController yarn)
    {
        for (int i = 0; i < connections.Count; i++)
        {
            bool firstCheck = false;
            bool secondCheck = false;

            if (firstID == connections[i].FirstID || firstID == connections[i].SecondID)
            {
                firstCheck = true;
            }
            else
            {
                firstCheck = false;
            }

            print(firstCheck + " " + firstID + " " + connections[i].FirstID + " " + connections[i].SecondID);

            if (secondID == connections[i].FirstID || secondID == connections[i].SecondID)
            {
                secondCheck = true;
            }
            else
            {
                secondCheck = false;
            }

            if (firstCheck && secondCheck)
            {
                yarn.IsConnecting = false;

                i = connections.Count + 1;
            }
        }
    }

    public void PrintList()
    {
        for (int i = 0; i < connections.Count;i++)
        {
            print(connections[i].FirstID + " " + connections[i].SecondID);
        }
    }
}
