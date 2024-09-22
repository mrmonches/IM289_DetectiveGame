using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileController : MonoBehaviour
{
    //Written by Quinn
    public EvidenceData heldDataFile;

    public void CloseFile()
    {
        Destroy(gameObject);
    }
}
