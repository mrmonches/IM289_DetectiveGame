using UnityEngine;

public class EvidenceController : MonoBehaviour
{
    private BoxCollider _boxCollider;

    private PlayerController _playerController;

    [SerializeField] private Vector3 OffsetPos;

    [SerializeField, Tooltip("Change this to adjust hover distance")]
    private Vector3 HoverPos;

    [SerializeField, Tooltip("Change this to make evidence follow mouse faster/smoother")] 
    private float SlerpSpeed;

    [SerializeField, Tooltip("Change this to make evidence hover faster/smoother")]
    private float HoverSpeed;

    private bool isHeld;
    private bool isHover;

    private Vector3 placedPos;

    public bool IsHeld { get => isHeld; set => isHeld = value; }
    public bool IsHover { get => isHover; set => isHover = value; }

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();

        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        RecordPlacedPos();
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

    /// <summary>
    /// Records last placed position
    /// Important for the hover functions
    /// </summary>
    public void RecordPlacedPos()
    {
        placedPos = transform.position;
    }

    private void LateUpdate()
    {
        if (IsHeld)
        {
            FollowPlayerMouse();
        }

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
