using UnityEngine;

public class FolderController : MonoBehaviour
{
    //Quinn wrote this
    //public EvidenceData heldData;
    [SerializeField] private GameObject file;
    [SerializeField] private Canvas canvas;

    private FileController _fileController;

    private bool folderOpen;

    public bool FolderOpen { get => folderOpen; set => folderOpen = value; }
    public FileController FileControl { get => _fileController; set => _fileController = value; }

    public void OpenCloseFile()
    {
        var openedFile = Instantiate(file);
        openedFile.transform.SetParent(canvas.transform, false);

        _fileController = openedFile.GetComponent<FileController>();
        
        //openedFile.GetComponent<FileController>().heldDataFile = heldData;
    }
}
