using System.Collections.Generic;
using UnityEngine;

public class YarnController : MonoBehaviour
{
    private LineRenderer lineRenderer;

    [SerializeField] private List<Material> YarnMaterial;

    [SerializeField] private List<ConnectionData> connectionData;

    private GameObject firstObject, secondObject;

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
    }

    public void GiveLinePosition(Vector3 pos)
    {
        lineRenderer.positionCount++;

        lineRenderer.SetPosition(lineRenderer.positionCount - 1, pos);
    }
}
