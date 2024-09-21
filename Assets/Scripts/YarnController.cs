using System.Collections.Generic;
using UnityEngine;

public class YarnController : MonoBehaviour
{
    private LineRenderer lineRenderer;

    [SerializeField] private List<Material> GoodConnectionMaterial;
    [SerializeField] private List<Material> BadConnectionMaterial;

    private EvidenceBoardManager boardManager;

    [SerializeField] private EvidenceID firstID, secondID;
    [SerializeField] private GameObject firstObject, secondObject;

    private bool isConnecting;

    [SerializeField] private GameObject LineObject;

    private PlayerController _playerController;

    public bool IsConnecting { get => isConnecting; set => isConnecting = value; }

    private void Awake()
    {
        boardManager = GameObject.Find("Board").GetComponent<EvidenceBoardManager>();

        firstID = EvidenceID.Default;
        secondID = EvidenceID.Default;

        _playerController = FindObjectOfType<PlayerController>();
    }

    private void CreateLineRender()
    {
        lineRenderer = Instantiate(LineObject).GetComponent<LineRenderer>();

        lineRenderer.gameObject.transform.parent = gameObject.transform;

        lineRenderer.positionCount = 0;

        IsConnecting = true;
    }

    private void ClearCurrentRef()
    {
        boardManager.Connections.Add(new ConnectionData(lineRenderer, firstID, secondID, firstObject, secondObject));

        lineRenderer = null;

        firstID = EvidenceID.Default;
        secondID = EvidenceID.Default;

        firstObject.GetComponent<EvidenceController>().IsConnected = true;
        secondObject.GetComponent<EvidenceController>().IsConnected = true;

        firstObject = null;
        secondObject = null;

        CreateLineRender();
    }

    public void GiveLinePosition(Vector3 pos, EvidenceID evidenceID, GameObject evidence)
    {
        lineRenderer.positionCount++;

        lineRenderer.SetPosition(lineRenderer.positionCount - 1, pos);

        if (firstID == EvidenceID.Default)
        {
            firstID = evidenceID;

            firstObject = evidence;
        }
        else
        {
            secondID = evidenceID;

            secondObject = evidence;

            ClearCurrentRef();
        }
    }

    private void CheckControllerStatus()
    {
        if (lineRenderer == null)
        {
            CreateLineRender();
        }

        if (lineRenderer.positionCount >= 2)
        {
            ClearCurrentRef();
        }
    }

    private void CheckConnectionStatus(EvidenceData evidenceID)
    {
        if (evidenceID.CheckCorrectConnection(firstID))
        {
            lineRenderer.SetMaterials(GoodConnectionMaterial);
        }
        else
        {
            lineRenderer.SetMaterials(BadConnectionMaterial);
        }
    }

    public void CheckLineStatus(Vector3 pos, EvidenceData evidenceID, GameObject evidence)
    {
        EvidenceID _id = evidenceID.EvidenceID;

        CheckControllerStatus();

        if (firstID != EvidenceID.Default)
        {
            CheckSelfConnection(_id);

            if (IsConnecting)
            {
                boardManager.CheckConnectionList(firstID, _id, GetComponent<YarnController>());
            }

            if (IsConnecting)
            {
                CheckConnectionStatus(evidenceID);
            }
        }

        if (IsConnecting)
        {
            GiveLinePosition(pos, _id, evidence);
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
        else
        {
            IsConnecting = true;
        }
    }
}
