using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceCardMenuBehavior : MonoBehaviour
{
    private bool menuActive;

    [SerializeField] private Canvas menuCanvas;

    public void SetCardMenuStatus(bool status)
    {
        menuActive = status;

        menuCanvas.gameObject.SetActive(status);
    }

    public bool GetCardMenuStatus()
    {
        return menuActive;
    }
}
