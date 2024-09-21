using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolderController : MonoBehaviour
{
    //Quinn wrote this
    public EvidenceData heldData;
    [SerializeField] GameObject file;
    [SerializeField] Canvas canvas;

    public void OpenCloseFile()
    {
        var openedFile = Instantiate(file);
        openedFile.transform.SetParent(canvas.transform, false);
        
        openedFile.GetComponent<FileController>().heldDataFile = heldData;
    }

}
