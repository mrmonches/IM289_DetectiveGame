using System.Collections.Generic;
using UnityEngine;

//By Nolan

public class EvidenceBoardManager : MonoBehaviour
{
    [SerializeField] private float EvidencePlacePos;
    [SerializeField] private List<ConnectionData> connections = new List<ConnectionData>();

    [SerializeField] private int ListCount;

    [SerializeField] private float YarnOffset;

    private bool hasShortList;

    public float EvidencePlacePos1 { get => EvidencePlacePos; private set => EvidencePlacePos = value; }
    public List<ConnectionData> Connections { get => connections; set => connections = value; }
    public bool HasShortList { get => hasShortList; set => hasShortList = value; }

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

    public void RemoveConnectionFromList(EvidenceID id)
    {
        for (int i = 0; i < connections.Count; i++)
        {
            if (id == connections[i].FirstID || id == connections[i].SecondID)
            {
                Destroy(connections[i].LineObject);
                connections.RemoveAt(i);
                i--;
            }
        }
    }

    public void RemoveConnectionFromList(LineRenderer lr)
    {
        for (int i = 0; i < connections.Count; i++)
        {
            if (lr == connections[i].LineRenderer)
            {
                Destroy(connections[i].LineObject);
                connections.RemoveAt(i);
                i--;
            }
        }
    }

    public void CheckCount()
    {
        ListCount = connections.Count;
    }

    public void PrintList()
    {
        for (int i = 0; i < connections.Count;i++)
        {
            print(connections[i].FirstID + " " + connections[i].SecondID);
        }
    }

    public void UpdateLinePos(GameObject evidence, EvidenceID evidenceID)
    {
        for(int i = 0; i < connections.Count; i++)
        {
            //print(connections[i].FirstID + " " + connections[i].SecondID + " " + evidenceID);
            if (connections[i].FirstID == evidenceID)
            {
                connections[i].LineRenderer.SetPosition(0, new Vector3(evidence.transform.position.x,
                    evidence.transform.position.y, evidence.transform.position.z + YarnOffset));
            }
            else if (connections[i].SecondID == evidenceID)
            {
                connections[i].LineRenderer.SetPosition(1, new Vector3(evidence.transform.position.x,
                    evidence.transform.position.y, evidence.transform.position.z + YarnOffset));
            }
        }
    }
}