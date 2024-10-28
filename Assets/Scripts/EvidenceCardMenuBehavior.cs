using TMPro;
using UnityEngine;

// By Nolan

public class EvidenceCardMenuBehavior : MonoBehaviour
{
    private bool menuActive;

    [SerializeField] private YarnController _yarnController;

    [SerializeField] private EvidenceController _evidenceController;

    private PlayerController _playerController;

    private EvidenceBoardManager _boardManager;

    [SerializeField] private Canvas menuCanvas;

    [SerializeField] private TMP_Text ConnectionButtonText;

    [SerializeField] private string StartConnectionText, EndConnectionText;

    private AudioSource _audioSource;

    [SerializeField] private AudioClip CutClip;

    private void OnEnable()
    {
        _evidenceController = GetComponent<EvidenceController>();

        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        _yarnController = GetComponent<YarnController>();

        _boardManager = FindObjectOfType<EvidenceBoardManager>();

        _audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Sets the card menu to active or inactive - depending on status
    /// </summary>
    public void SetCardMenuStatus(bool status)
    {
        menuActive = status;

        menuCanvas.gameObject.SetActive(status);

        if (!_playerController.IsConnecting)
        {
            ConnectionButtonText.text = StartConnectionText;
        }
        else 
        {
            ConnectionButtonText.text = EndConnectionText;
        }
    }

    public bool GetCardMenuStatus()
    {
        return menuActive;
    }

    public void StartConnectionButton()
    {
        if (!_playerController.IsConnecting)
        {
            _playerController.AssignYarnController(_yarnController);

            _yarnController.CheckLineStatus(_evidenceController.ChildTransform.position,
                        _evidenceController.EvidenceData, _evidenceController.gameObject);
        }
        else
        {
            _playerController.GetYarnController().CheckLineStatus(_evidenceController.ChildTransform.position,
                        _evidenceController.EvidenceData, _evidenceController.gameObject);

            _playerController.UnassignYarnController();
        }

        SetCardMenuStatus(false);
    }

    public void RemoveConnectionsButton()
    {
        _boardManager.RemoveConnectionFromList(_evidenceController.ID);

        _audioSource.PlayOneShot(CutClip);

        SetCardMenuStatus(false);
    }

    public void RemoveCardFromBoard()
    {
        RemoveConnectionsButton();

        Destroy(gameObject);
    }
}
