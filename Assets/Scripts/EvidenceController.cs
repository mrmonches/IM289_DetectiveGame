using TMPro;
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
    private Vector3 HoverPos, InHandHoverPos;

    [SerializeField, Tooltip("Change this to make evidence follow mouse faster/smoother")] 
    private float SlerpSpeed;

    [SerializeField, Tooltip("Change this to make evidence hover faster/smoother")]
    private float HoverSpeed;

    [SerializeField] private bool isHeld;
    private bool isHover;
    private bool isConnected;
    [SerializeField] private bool cancelHover;

    [SerializeField] private bool canPlace;

    private Vector3 placedPos;

    private EvidenceBoardManager _boardManager;

    [SerializeField] private Transform _childTransform;

    [SerializeField] private EvidenceData _evidenceData;

    [SerializeField] private Vector3 direction;

    [SerializeField] private TMP_Text CardText;

    [SerializeField] private Vector3 CastScale, CastOffsetPos;

    private EvidenceID _id;

    private EvidenceCardMenuBehavior _menuBehavior;

    private bool isInHand;

    [SerializeField] private bool CantDelete;

    public bool IsHeld { get => isHeld; set => isHeld = value; }
    public bool IsHover { get => isHover; set => isHover = value; }
    public Transform ChildTransform { get => _childTransform; private set => _childTransform = value; }
    public EvidenceID ID { get => _id; set => _id = value; }
    public EvidenceData EvidenceData { get => _evidenceData; set => _evidenceData = value; }
    public bool IsConnected { get => isConnected; set => isConnected = value; }
    public EvidenceCardMenuBehavior MenuBehavior { get => _menuBehavior; set => _menuBehavior = value; }
    public bool IsInHand { get => isInHand; set => isInHand = value; }

    private void OnEnable()
    {
        _boxCollider = GetComponent<BoxCollider>();

        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        _boardManager = FindObjectOfType<EvidenceBoardManager>();

        RecordPlacedPos();

        _menuBehavior = GetComponent<EvidenceCardMenuBehavior>();

        if (!CantDelete)
        {
            IsInHand = true;
        }
        else
        {
            isInHand = false;
            OnPlace();
            _id = EvidenceData.EvidenceID;

        }
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
        transform.position = Vector3.Slerp(transform.position, placedPos, (HoverSpeed * 2) * Time.deltaTime);
    }

    public void OnPlace()
    {
        IsHeld = false;

        if (canPlace)
        {
            transform.position = new Vector3 (transform.position.x, transform.position.y, _boardManager.EvidencePlacePos1);

            transform.parent = _boardManager.transform;

            RecordPlacedPos();

            if (IsInHand)
            {
                IsInHand = false;

                EvidenceStackManager evidenceStackManager = GameObject.Find("EvidenceWall").GetComponent<EvidenceStackManager>();

                evidenceStackManager.RemoveFromStack(_evidenceData, gameObject);
            }

            if (isConnected)
            {
                _boardManager.UpdateLinePos(gameObject, _id);
            }
        }
        else
        {
            transform.position = placedPos;

            if (isConnected)
            {
                _boardManager.UpdateLinePos(gameObject, _id);
            }
        }
    }

    public void GiveEvidenceData(EvidenceData data)
    {
        _evidenceData = data;

        _id = _evidenceData.EvidenceID;

        if (_evidenceData.EvidenceType == EvidenceType.Document)
        {
            CardText.text = _evidenceData.GetCardInformation;
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
        Vector3 pos = new Vector3(transform.position.x + CastOffsetPos.x,
                transform.position.y + CastOffsetPos.y,
                transform.position.z + CastOffsetPos.z);

        RaycastHit hit;
        if (Physics.BoxCast(pos, CastScale, direction, out hit, transform.rotation, CastDistance, EvidenceMask) && 
            hit.collider != _boxCollider)
        {
            canPlace = false;
        }
        else
        {
            canPlace = true;
        }
    }

    // Uncomment this function if the box cast that makes the cards not overlap is not working

    //private void OnDrawGizmos()
    //{
    //    Vector3 pos = new Vector3(transform.position.x + CastOffsetPos.x,
    //        transform.position.y + CastOffsetPos.y,
    //        transform.position.z + CastOffsetPos.z);

    //    RaycastHit hit;
    //    if (Physics.BoxCast(pos, CastScale, direction, out hit, transform.rotation, CastDistance, EvidenceMask))
    //    {
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawRay(pos, direction * hit.distance);
    //        Gizmos.DrawWireCube(pos + direction * hit.distance, CastScale);

    //        canPlace = false;
    //    }
    //    else
    //    {
    //        Gizmos.color = Color.green;
    //        Gizmos.DrawRay(pos, direction * CastDistance);
    //        Gizmos.DrawWireCube(pos + direction * hit.distance, CastScale);

    //        canPlace = true;
    //    }
    //}

    public bool DeleteStatus()
    {
        return CantDelete;
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

            if (isConnected)
            {
                _boardManager.UpdateLinePos(gameObject, _id);
            }
        }
        else if (!IsInHand)
        {
            if (IsHover && transform.position != placedPos + HoverPos)
            {
                OnHover();
            }
            else if (!IsHover && transform.position != placedPos && !cancelHover)
            {
                OnUnhover();
            }
        }
    }
}
