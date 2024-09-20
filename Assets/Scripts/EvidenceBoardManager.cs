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
        List<ConnectionData> tempList = connections;

        for (int i = 0; i < tempList.Count; i++)
        {
            bool firstCheck, secondCheck;

            if (firstID == connections[i].FirstID || firstID == connections[i].SecondID)
            {
                firstCheck = true;
            }
            else
            {
                firstCheck = false;
            }

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
            }
            else
            {
                tempList.RemoveAt(i);
                i--;
            }
        }
    }
}
