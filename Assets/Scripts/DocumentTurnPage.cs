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

    private PlayerController _playerController;

    [SerializeField] private bool isIntroBook;

    public bool IsIntroBook { get => isIntroBook; set => isIntroBook = value; }

    private void OnEnable()
    {
        canvas = FindObjectOfType<Canvas>();
        gameObject.GetComponentInParent<DocumentTurnPage>();
        _systemManager = FindObjectOfType<SystemManager>();

        _playerController = FindObjectOfType<PlayerController>();
    }

    public void TurnPage()
    {
        _nextPage.SetActive(true);

        GivePlayerDocument(_nextPage);

        gameObject.SetActive(false);
    }

    public void BackPage()
    {
        _previousPage.SetActive(true);

        GivePlayerDocument(_previousPage);

        gameObject.SetActive(false);
    }

    private void GivePlayerDocument(GameObject newPage)
    {
        _playerController.SetDocument(newPage.GetComponent<DocumentTurnPage>());
    }

    public void CloseDoc()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().InItemViewer = false;
        _systemManager.updatePaperOpen(false,"null");
        gameObject.SetActive(false);
    }
}
