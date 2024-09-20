using System.Collections.Generic;
using UnityEngine;

public class YarnController : MonoBehaviour
{
    private LineRenderer lineRenderer;

    [SerializeField] private List<Material> YarnMaterial;

    private EvidenceBoardManager boardManager;

    private EvidenceID firstID, secondID;

    private bool isConnecting;

    public bool IsConnecting { get => isConnecting; set => isConnecting = value; }

    private void Awake()
    {
        if (gameObject.GetComponent<LineRenderer>() == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();

            lineRenderer.SetMaterials(YarnMaterial);

            lineRenderer.positionCount = 0;
        }

        boardManager = GameObject.Find("EvidenceBoard").GetComponent<EvidenceBoardManager>();
    }

    // if line renderer positioncount = 2, clear references, create new line renderer, add another position, set position

    // if line renderer position count > 2, add another position, set position


    public void GiveLinePosition(Vector3 pos, EvidenceID evidenceID)
    {
        if (lineRenderer.positionCount <= 2)
        {
            boardManager.Connections.Add(new ConnectionData(lineRenderer, firstID, secondID));

            lineRenderer = null;

            firstID = EvidenceID.Null;
            secondID = EvidenceID.Null;

            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        lineRenderer.positionCount++;

        lineRenderer.SetPosition(lineRenderer.positionCount - 1, pos);

        if (firstID == EvidenceID.Null)
        {
            firstID = evidenceID;
        }
        else
        {
            secondID = evidenceID;
        }
    }

    public void CheckLineStatus(Vector3 pos, EvidenceID evidenceID)
    {
        if (firstID != EvidenceID.Null)
        {
            CheckSelfConnection(evidenceID);

            if (IsConnecting)
            {
                boardManager.CheckConnectionList(firstID, evidenceID, GetComponent<YarnController>());
            }
        }

        if (IsConnecting)
        {
            GiveLinePosition(pos, evidenceID);
        }
        else
        {
            firstID = EvidenceID.Null;
            secondID = EvidenceID.Null;

            lineRenderer.positionCount = 0;
        }
    }

    public void CheckSelfConnection(EvidenceID evidenceID)
    {
        if (evidenceID == firstID)
        {
            IsConnecting = false;

            firstID = EvidenceID.Null;
        }
    }
}
