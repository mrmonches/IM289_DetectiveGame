using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceCardMenuBehavior : MonoBehaviour
{
    private bool menuActive;

    [SerializeField] private Canvas menuCanvas;

    public void ActivateCardMenu()
    {
        menuActive = true;

        menuCanvas.gameObject.SetActive(true);
    }
}
