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

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
        gameObject.GetComponentInParent<DocumentTurnPage>();
    }
    public void TurnPage()
    {
        var openedpage = Instantiate(_nextPage);
        openedpage.transform.SetParent(canvas.transform, false);

        Destroy(gameObject);
    }

    public void BackPage()
    {
        var openedpage = Instantiate(_previousPage);
        openedpage.transform.SetParent(canvas.transform, false);

        Destroy(gameObject);
    }
}
