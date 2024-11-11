using UnityEngine;

public class FolderController : MonoBehaviour
{
    //Quinn wrote this
    //public EvidenceData heldData;
    [SerializeField] private GameObject file;
    [SerializeField] private Canvas canvas;

    private FileController _fileController;
    private SystemManager _systemManager;
    private PlayerController _playerController;


    private bool folderOpen;

    public bool FolderOpen { get => folderOpen; set => folderOpen = value; }
    public FileController FileControl { get => _fileController; set => _fileController = value; }

    public void OpenFile()
    {
        file.SetActive(true);
        _playerController.getOpenDoc(file);
        
        //openedFile.GetComponent<FileController>().heldDataFile = heldData;
    }

    public void CloseFile()
    {
        _playerController.InItemViewer = false;
        file.SetActive(false);
    }

    private void Awake()
    {
        _systemManager = FindObjectOfType<SystemManager>();

        _playerController = FindObjectOfType<PlayerController>();
    }
}
