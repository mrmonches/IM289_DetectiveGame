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
        int selectedIndex = dropdown1.value;
        string selectedOption = dropdown1.options[selectedIndex].text;
        if (selectedOption== "The Alleway")
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
        int selectedIndex = dropdown1.value;
        string selectedOption = dropdown1.options[selectedIndex].text;
        if (selectedOption== "A large frame revolver")
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

        if (selectedOption == "Friday 13th 1942")
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

        if (selectedOption == "Jealous of Kings Life")
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
    public void CorrectOption(EvidenceID FirstID,EvidenceData SecondData)
    {
        //yeahh this code is horrible sorry Zach. I amn going to improve it in a later milestone
        EvidenceID SecondID = SecondData.EvidenceID;
        if ( FirstID== EvidenceID.A01_02 || SecondID== EvidenceID.A01_02)
        {
            dropdown1.options.Add(new TMP_Dropdown.OptionData("Darling King", null));
            dropdown1.RefreshShownValue();
        }
        else if (FirstID== EvidenceID.A01_04 || SecondID == EvidenceID.A01_04)
        {
            dropdown1.options.Add(new TMP_Dropdown.OptionData("Steven Knight", null));
            dropdown1.RefreshShownValue();
        }
        else if (FirstID == EvidenceID.A01_07 || SecondID == EvidenceID.A01_07)
        {
            dropdown1.options.Add(new TMP_Dropdown.OptionData("Guioco Piano", null));
            dropdown1.RefreshShownValue();
        }
        else if (FirstID == EvidenceID.A04_02||SecondID==EvidenceID.A04_02)
        {
            dropdown5.options.Add(new TMP_Dropdown.OptionData("was a rat", null));
            dropdown5.RefreshShownValue();
        }
        else if(FirstID==EvidenceID.A03_02||SecondID==EvidenceID.A03_02 || FirstID == EvidenceID.A03_01 || SecondID == EvidenceID.A03_01)
        {
            dropdown5.options.Add(new TMP_Dropdown.OptionData("Wanted their inheritance from King", null));
            dropdown5.RefreshShownValue();
        }
        else if(FirstID==EvidenceID.A04_05||SecondID==EvidenceID.A04_05)
        {
            dropdown3.options.Add(new TMP_Dropdown.OptionData("The Red Stiletto", null));
            dropdown3.RefreshShownValue();
        }
        else if (FirstID == EvidenceID.A02_01 || SecondID == EvidenceID.A02_01)
        {
            dropdown3.options.Add(new TMP_Dropdown.OptionData("The Alleway", null));
            dropdown3.RefreshShownValue();
        }
        else if (FirstID == EvidenceID.A01_09 || SecondID == EvidenceID.A01_09)
        {
            dropdown4.options.Add(new TMP_Dropdown.OptionData("A large frame revolver", null));
            dropdown4.RefreshShownValue();
        }
        else if (FirstID == EvidenceID.A04_04 || SecondID == EvidenceID.A04_04)
        {
            dropdown4.options.Add(new TMP_Dropdown.OptionData("Jealous of Kings Life", null));
            dropdown4.RefreshShownValue();
        }
    }
    private void Awake()
    {
        typewriterStuff.gameObject.SetActive(false);
    }
}
