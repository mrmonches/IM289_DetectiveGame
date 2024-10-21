// Nolan wrote this script
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
    private TypeWriterController _typewriterController;

    // Preferences on line renderer, don't change unless you know what you want from the line renderer
    [SerializeField, Tooltip("Sets width of the line renderer")] private float LineWidth;
    [SerializeField, Tooltip("Adjusts texture stretch of line renderer")] private Vector2 LineScale;

    public bool IsConnecting { get => isConnecting; set => isConnecting = value; }

    private void Awake()
    {
        boardManager = FindObjectOfType<EvidenceBoardManager>();

        firstID = EvidenceID.Default;
        secondID = EvidenceID.Default;

        _playerController = FindObjectOfType<PlayerController>();
        _typewriterController = FindObjectOfType<TypeWriterController>();
    }

    /// <summary>
    /// This creates an empty line renderer
    /// This also handles most of the default settings for the line renderer
    /// </summary>
    private void CreateLineRender()
    {
        lineRenderer = Instantiate(LineObject).GetComponent<LineRenderer>();

        lineRenderer.gameObject.transform.parent = gameObject.transform;

        lineRenderer.positionCount = 0;

        lineRenderer.startWidth = LineWidth;
        lineRenderer.endWidth = LineWidth;

        lineRenderer.textureScale = LineScale;

        IsConnecting = true;
    }

    /// <summary>
    /// Clears all current references so that new connection can be made
    /// </summary>
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

    /// <summary>
    /// Gives current line position so that a line can be successfully drawn
    /// Should not directly be called, since there are checks that must be made
    /// </summary>
    private void GiveLinePosition(Vector3 pos, EvidenceID evidenceID, GameObject evidence)
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

    /// <summary>
    /// Important check that makes sure:
    /// The line renderer doesn't have more than three positions
    /// And if the line renderer is cleared, a new one is created
    /// </summary>
    private void CheckControllerStatus()
    {
        if (lineRenderer == null)
        {
            CreateLineRender();
        }

        if (lineRenderer.positionCount >= 2)
        {
            ClearCurrentRef();

            _playerController.IsConnecting = false;
        }

        if (lineRenderer.positionCount == 0 && !isConnecting)
        {
            isConnecting = true;
        }
    }

    /// <summary>
    /// This method makes the line black or red
    /// Checks if the connection is a "good" or "bad" one
    /// </summary>
    private void CheckConnectionStatus(EvidenceData evidenceID)
    {
        if (evidenceID.CheckCorrectConnection(firstID))
        {
            lineRenderer.SetMaterials(GoodConnectionMaterial);
            _typewriterController.CorrectOption(firstID,evidenceID);

        }
        else
        {
            lineRenderer.SetMaterials(BadConnectionMaterial);
        }
    }

    /// <summary>
    /// This is the function that should be called if the player is creating a connection
    /// 
    /// Conducts most of the checks including:
    /// Whether or not there is an active line renderer
    /// If the player is trying to create a self connection
    /// If the connection has already been made
    /// </summary>
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

    /// <summary>
    /// Checks if the connection is trying to connect to itself
    /// </summary>
    private void CheckSelfConnection(EvidenceID evidenceID)
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

    public void ClearUnfinishedConnection()
    {
        firstID = EvidenceID.Default;
        secondID = EvidenceID.Default;

        firstObject = null;
        secondObject = null;

        lineRenderer.positionCount = 0;
    }
}
