using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionData
{
    private LineRenderer lineRenderer;
    private EvidenceID firstID, secondID;

    public ConnectionData(LineRenderer lineRenderer, EvidenceID firstID, EvidenceID secondID, GameObject firstObject, GameObject secondObject)
    {
        this.LineRenderer = lineRenderer;
        this.firstID = firstID;
        this.secondID = secondID;
    }

    public EvidenceID FirstID { get => firstID; set => firstID = value; }
    public EvidenceID SecondID { get => secondID; set => secondID = value; }
    public LineRenderer LineRenderer { get => lineRenderer; set => lineRenderer = value; }
}
