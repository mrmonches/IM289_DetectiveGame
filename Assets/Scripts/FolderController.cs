using UnityEngine;

public class FolderController : MonoBehaviour
{
    //Quinn wrote this
    //public EvidenceData heldData;
    [SerializeField] private GameObject file;
    [SerializeField] private Canvas canvas;
 

    private bool folderOpen;

    public bool FolderOpen { get => folderOpen; set => folderOpen = value; }

    public void OpenCloseFile()
    {

            var openedFile = Instantiate(file);
            openedFile.transform.SetParent(canvas.transform, false);
 


        
        //openedFile.GetComponent<FileController>().heldDataFile = heldData;
    }
}
