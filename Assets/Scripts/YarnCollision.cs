using System.Collections.Generic;
using UnityEngine;


// Nolan wrote this script
public class YarnCollision : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    private List<Vector2> collisionPoints = new List<Vector2>();

    private PolygonCollider2D _polygonCollider;

    [SerializeField] private float WidthOffset;

    private bool startCalculating;

    public bool StartCalculating { get => startCalculating; set => startCalculating = value; }

    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();

        _polygonCollider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startCalculating)
        {
            collisionPoints = CalculateColliderPoints();

            _polygonCollider.SetPath(0, collisionPoints.ConvertAll(p => (Vector2)transform.InverseTransformPoint(p)));
        }
    }

    /// <summary>
    /// Some british man showed me this math and I don't fully understand it, 
    /// I passed geometry in high school
    /// </summary>
    /// <returns></returns>
    private List<Vector2> CalculateColliderPoints()
    {
        Vector3[] positions = GetPositions();

        float width = _lineRenderer.startWidth + WidthOffset;

        // Equations getting the slope between the points so that the collision points can be set
        float m = (positions[1].y - positions[0].y) / (positions[1].x - positions[0].x);
        float deltaX = (width / 2f) * (m / Mathf.Pow(m * m + 1, 0.5f));
        float deltaY = (width / 2f) * (1 / Mathf.Pow(1 + m * m, 0.5f));

        // Sets offset for each point for the collision
        Vector3[] offset = new Vector3[2];
        offset[0] = new Vector3(-deltaX, deltaY);
        offset[1] = new Vector3(deltaX, -deltaY);

        // Add Collider Points
        List<Vector2> collisionPoints = new List<Vector2> 
           {positions[0] + offset[0], 
            positions[1] + offset[0], 
            positions[1] + offset[1], 
            positions[0] + offset[1]};

        return collisionPoints;
    }

    /// <summary>
    /// Returns all positions on the line renderer
    /// </summary>
    private Vector3[] GetPositions()
    {
        Vector3[] positions = new Vector3[_lineRenderer.positionCount];
        _lineRenderer.GetPositions(positions);
        return positions;
    }

    public void EndCollisions()
    {
        EvidenceBoardManager _boardManager = FindObjectOfType<EvidenceBoardManager>();

        _boardManager.RemoveConnectionFromList(_lineRenderer);

        Destroy(gameObject);
    }
}
