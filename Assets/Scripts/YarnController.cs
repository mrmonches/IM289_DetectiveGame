using System.Collections.Generic;
using UnityEngine;

public class YarnController : MonoBehaviour
{
    private LineRenderer lineRenderer;

    [SerializeField] private List<Material> YarnMaterial;

    private EvidenceBoardManager boardManager;

    private EvidenceID firstID, secondID;

    private bool isConnecting;

    [SerializeField] private GameObject LineObject;

    private PlayerController _playerController;

    public bool IsConnecting { get => isConnecting; set => isConnecting = value; }

    private void Awake()
    {
        if (gameObject.GetComponent<LineRenderer>() == null)
        {
            CreateLineRender();
        }

        boardManager = GameObject.Find("Board").GetComponent<EvidenceBoardManager>();

        firstID = EvidenceID.Default;
        secondID = EvidenceID.Default;

        _playerController = FindObjectOfType<PlayerController>();
    }

    private void CreateLineRender()
    {
        lineRenderer = Instantiate(LineObject).GetComponent<LineRenderer>();

        lineRenderer.gameObject.transform.parent = gameObject.transform;

        lineRenderer.SetMaterials(YarnMaterial);

        lineRenderer.positionCount = 0;
    }

    public void GiveLinePosition(Vector3 pos, EvidenceID evidenceID)
    {
        if (lineRenderer.positionCount >= 2)
        {
            boardManager.Connections.Add(new ConnectionData(lineRenderer, firstID, secondID));
            //boardManager.PrintList();

            lineRenderer = null;

            firstID = EvidenceID.Default;
            secondID = EvidenceID.Default;

            //lineRenderer = gameObject.AddComponent<LineRenderer>();

            CreateLineRender();
        }

        lineRenderer.positionCount++;

        lineRenderer.SetPosition(lineRenderer.positionCount - 1, pos);

        if (firstID == EvidenceID.Default)
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
        if (firstID != EvidenceID.Default)
        {
            CheckSelfConnection(evidenceID);

            if (IsConnecting && boardManager.Connections.Count > 1)
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
            firstID = EvidenceID.Default;
            secondID = EvidenceID.Default;

            lineRenderer.positionCount = 0;
        }
    }

    public void CheckSelfConnection(EvidenceID evidenceID)
    {
        if (evidenceID == firstID)
        {
            IsConnecting = false;

            firstID = EvidenceID.Default;
        }
    }
}
