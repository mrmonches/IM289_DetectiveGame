using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriterController : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown1;
    [SerializeField] private TMP_Dropdown dropdown2;
    [SerializeField] private TMP_Dropdown dropdown3;
    [SerializeField] private TMP_Dropdown dropdown4;
    [SerializeField] private TMP_Dropdown dropdown5;
    [SerializeField] private GameObject typewriter;
    [SerializeField] private Button submit;
    [SerializeField] private Button backToDesk;
    [SerializeField] private TMP_Text winLoseText;
    private int option1;
    private int option2;
    private int option3;
    private bool correctCheck1=false;
    private bool correctCheck2=false;
    private bool correctCheck3=false;
    private bool crorrectCheck4 = false;
    private bool crorrectCheck5 = false;
    private bool finalCorrectCheck=false;
    [SerializeField] private EvidenceData _evidenceData;
    [SerializeField] private GameObject typewriterStuff;
    //This is used to prevent moving between stations when in the typewriter 
    [SerializeField] private GameObject _cameraController;
    

    /// <summary>
    /// Changed this to new input system and moved it's activation to the PlayerController
    /// </summary>
    private void ShowCanvas()
    {
        _cameraController.GetComponent<CameraController>().getCannotMove();
        typewriterStuff.gameObject.SetActive(true);
        
    }

    public void GetShowCanvas()
    {
        ShowCanvas();
        typewriterStuff.gameObject.SetActive(true); 
    }
    //checks if all the dropdowns are correct;
    public void Dropdown1(int val)
    {
        int selectedIndex = dropdown1.value;
        string selectedOption = dropdown1.options[selectedIndex].text;
        
        if (selectedOption=="Steven Knight")
        { 
            correctCheck1 = true;
            
            
        }
        else
        {
            correctCheck1 = false;            
        }
    }
    public void Dropdown2(int val)
    {
        if (val == 1)
        {
            correctCheck2 = true;
           

        }
        else if (val == 0||val==2)
        {
            correctCheck2 = false;
           
        }
    }
    public void Dropdown3(int val)
    {
        if (val == 2)
        {
            correctCheck3 = true;
            

        }
        else if(val==1||val==0)
        {
            correctCheck3 = false;
            
        }
    }
    public void Dropdown4(int val)
    {
        int selectedIndex = dropdown4.value;
        string selectedOption = dropdown1.options[selectedIndex].text;

        if (selectedOption == "Steven Knight")
        {
            correctCheck1 = true;


        }
        else
        {
            correctCheck1 = false;
        }
    }
    public void Dropdown5(int val)
    {
        int selectedIndex = dropdown5.value;
        string selectedOption = dropdown1.options[selectedIndex].text;

        if (selectedOption == "Steven Knight")
        {
            correctCheck1 = true;


        }
        else
        {
            correctCheck1 = false;
        }
    }
    public void SubmitConclusion()
    {
        if (correctCheck1 == true && correctCheck2 == true && correctCheck3 == true)
        {
            finalCorrectCheck = true;
           
        }
        else
        {
            finalCorrectCheck = false;
        }
        typewriterStuff.gameObject.SetActive(false);
        //replaced with newspaper past prototype

        if (finalCorrectCheck == true)
        {
            winLoseText.text = "You locked the criminal behind bars!";
        }
        else if(finalCorrectCheck==false)
        {
            winLoseText.text = "Looks like the criminal got away";
        }
        winLoseText.gameObject.SetActive(true);
    }
    public void BackToDesk()
    {
        //Lets the player move after leaving
        _cameraController.GetComponent<CameraController>().getCanMove();

        typewriterStuff.gameObject.SetActive(false);
        
        
    }
    public void CorrectOption(EvidenceData evidenceData)
    {
        EvidenceID _id = evidenceData.EvidenceID;
        if (_id == EvidenceID.A01_01|| _id == EvidenceID.A01_02)
        {
            dropdown1.options.Add(new TMP_Dropdown.OptionData("Queen Bee", null));
            dropdown1.RefreshShownValue();
        }
        else if (_id == EvidenceID.A01_03|| _id == EvidenceID.A01_04)
        {
            dropdown1.options.Add(new TMP_Dropdown.OptionData("Steven Knight", null));
            dropdown1.RefreshShownValue();
        }
    }
    private void Awake()
    {
        typewriterStuff.gameObject.SetActive(false);
    }
}
