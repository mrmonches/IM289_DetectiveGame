using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentTurnPage : MonoBehaviour
{
    //Written by Quinn 10/4
    [SerializeField] private GameObject _nextPage;
    [SerializeField] private GameObject _previousPage;

    private GameObject parent;
    private Canvas canvas;

    private SystemManager _systemManager;
    

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
        gameObject.GetComponentInParent<DocumentTurnPage>();
        _systemManager = FindObjectOfType<SystemManager>();
    }

    public void TurnPage()
    {
        _nextPage.SetActive(true);

        gameObject.SetActive(false);
    }

    public void BackPage()
    {
        _previousPage.SetActive(true);
        
        gameObject.SetActive(false);
    }

    public void CloseDoc()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().InItemViewer = false;
        _systemManager.updatePaperOpen(false,"null");
        gameObject.SetActive(false);
    }
}
