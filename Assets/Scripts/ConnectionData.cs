// Nolan wrote this script
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionData
{
    private LineRenderer lineRenderer;
    private EvidenceID firstID, secondID;
    private GameObject lineObject;

    public ConnectionData(LineRenderer lineRenderer, GameObject lineObject, EvidenceID firstID, EvidenceID secondID, GameObject firstObject, GameObject secondObject)
    {
        this.LineRenderer = lineRenderer;
        this.lineObject = lineObject;
        this.firstID = firstID;
        this.secondID = secondID;
    }

    public EvidenceID FirstID { get => firstID; set => firstID = value; }
    public EvidenceID SecondID { get => secondID; set => secondID = value; }
    public LineRenderer LineRenderer { get => lineRenderer; set => lineRenderer = value; }
    public GameObject LineObject { get => lineObject; set => lineObject = value; }
}
