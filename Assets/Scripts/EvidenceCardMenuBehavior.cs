using UnityEngine;

// By Nolan

public class EvidenceCardMenuBehavior : MonoBehaviour
{
    private bool menuActive;

    private YarnController _yarnController;

    private EvidenceController _evidenceController;

    private EvidenceBoardManager _boardManager;

    [SerializeField] private Canvas menuCanvas;

    private void OnEnable()
    {
        _evidenceController = gameObject.GetComponent<EvidenceController>();

        _yarnController = gameObject.GetComponent<YarnController>();

        _boardManager = FindObjectOfType<EvidenceBoardManager>();
    }

    /// <summary>
    /// Sets the card menu to active or inactive - depending on status
    /// </summary>
    public void SetCardMenuStatus(bool status)
    {
        menuActive = status;

        menuCanvas.gameObject.SetActive(status);
    }

    public bool GetCardMenuStatus()
    {
        return menuActive;
    }

    public void StartConnectionButton()
    {
        _yarnController.CheckLineStatus(_evidenceController.ChildTransform.position,
            _evidenceController.EvidenceData, _evidenceController.gameObject);
    }

    public void RemoveConnectionsButton()
    {
        _boardManager.RemoveConnectionFromList(_evidenceController.ID);
    }

    public void RemoveCardFromBoard()
    {
        RemoveConnectionsButton();

        Destroy(gameObject);
    }
}
