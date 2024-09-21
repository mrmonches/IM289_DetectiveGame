using UnityEngine;

// By Nolan

public class EvidenceController : MonoBehaviour
{
    private BoxCollider _boxCollider;

    private PlayerController _playerController;

    [SerializeField] private float CastDistance;
    [SerializeField] private LayerMask EvidenceMask;

    [SerializeField] private Vector3 OffsetPos;

    [SerializeField, Tooltip("Change this to adjust hover distance")]
    private Vector3 HoverPos;

    [SerializeField, Tooltip("Change this to make evidence follow mouse faster/smoother")] 
    private float SlerpSpeed;

    [SerializeField, Tooltip("Change this to make evidence hover faster/smoother")]
    private float HoverSpeed;

    private bool isHeld;
    private bool isHover;

    private bool canPlace;

    private Vector3 placedPos;

    public bool IsHeld { get => isHeld; set => isHeld = value; }
    public bool IsHover { get => isHover; set => isHover = value; }

    private EvidenceBoardManager _boardManager;

    [SerializeField] private Transform _childTransform;
    public Transform ChildTransform { get => _childTransform; private set => _childTransform = value; }

    [SerializeField] private EvidenceData _evidenceData;

    private EvidenceID _id;
    public EvidenceID ID { get => _id; set => _id = value; }
    public EvidenceData EvidenceData { get => _evidenceData; set => _evidenceData = value; }

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();

        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        _boardManager = FindObjectOfType<EvidenceBoardManager>();

        RecordPlacedPos();

        _id = _evidenceData.EvidenceID;
    }

    /// <summary>
    /// Makes the evidence follow the mouse movement
    /// </summary>
    private void FollowPlayerMouse()
    {
        transform.position = Vector3.Slerp(transform.position, AdjustedMousePos(), SlerpSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Calculates an adjusted mouse position
    /// Not necessary, but included since it could be helpful depending on player feedback
    /// </summary>
    /// <returns></returns>
    private Vector3 AdjustedMousePos()
    {
        return new Vector3(_playerController.GetSelectedPosition().x + OffsetPos.x,
            _playerController.GetSelectedPosition().y + OffsetPos.y,
            _playerController.GetSelectedPosition().z + OffsetPos.z);
    }

    /// <summary>
    /// Makes the evidence hover on mouse hover
    /// </summary>
    private void OnHover()
    {
        transform.position = Vector3.Slerp(transform.position, placedPos + HoverPos, HoverSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Returns evidence to original position after player unhovers
    /// </summary>
    private void OnUnhover()
    {
        transform.position = Vector3.Slerp(transform.position, placedPos, HoverSpeed * Time.deltaTime);
    }

    public void OnPlace()
    {
        IsHeld = false;

        if (canPlace)
        {
            transform.position = new Vector3 (transform.position.x, transform.position.y, _boardManager.EvidencePlacePos1);

            RecordPlacedPos();
        }
        else
        {
            transform.position = placedPos;
        }
    }

    /// <summary>
    /// Records last placed position
    /// Important for the hover functions
    /// </summary>
    public void RecordPlacedPos()
    {
        placedPos = transform.position;
    }

    private void CheckPlacePos()
    {
        RaycastHit hit;
        if (Physics.BoxCast(transform.position, transform.lossyScale / 2, transform.forward, out hit, transform.rotation, CastDistance, EvidenceMask))
        {
            canPlace = false;
        }
        else
        {
            canPlace = true;
        }
    }

    private void LateUpdate()
    {
        if (IsHeld)
        {
            if (IsHover)
            {
                IsHover = false;
            }

            FollowPlayerMouse();

            CheckPlacePos();
        } 
        else
        {
            if (IsHover && transform.position != placedPos + HoverPos)
            {
                OnHover();
            }
            else if (!IsHover && transform.position != placedPos)
            {
                OnUnhover();
            }
        }
    }
}
