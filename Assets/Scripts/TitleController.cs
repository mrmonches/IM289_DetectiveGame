
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    [SerializeField] private GameObject mainTitle;
    [SerializeField] private GameObject controls;
    [SerializeField] private GameObject credits;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("TitleScene");
    }
    public void ShowControls()
    {
        mainTitle.gameObject.SetActive(false);
        controls.gameObject.SetActive(true);
    }
    public void ShowMain()
    {
        mainTitle.gameObject.SetActive(true);
        controls.gameObject.SetActive(false);
        credits.gameObject.SetActive(false);
    }
    public void ShowCredits()
    {
        mainTitle.gameObject.SetActive(false);
        credits.gameObject.SetActive(true);
    }
}
