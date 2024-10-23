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
    private bool correctCheck4 = false;
    private bool correctCheck5 = false;
    private bool finalCorrectCheck=false;
    [SerializeField] private EvidenceData _evidenceData;
    [SerializeField] private GameObject typewriterStuff;
    //This is used to prevent moving between stations when in the typewriter 
    [SerializeField] private GameObject _cameraController;
    private TitleFadeAway _titleFadeAway;

    private AudioSource _audioSource;

    [SerializeField] private AudioClip TypewriterClip;
    

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

    public void PlayTypewriterSound()
    {
       // _audioSource.PlayOneShot(TypewriterClip);
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
        int selectedIndex = dropdown2.value;
        string selectedOption = dropdown2.options[selectedIndex].text;
        if (selectedOption== "The Alleyway")
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
        int selectedIndex = dropdown3.value;
        string selectedOption = dropdown3.options[selectedIndex].text;
        if (selectedOption== "11:22PM")
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
        string selectedOption = dropdown4.options[selectedIndex].text;

        if (selectedOption == "A large frame revolver")
        {
            
            correctCheck4 = true;


        }
        else
        {
            correctCheck4 = false;
        }
    }
    public void Dropdown5(int val)
    {
        int selectedIndex = dropdown5.value;
        string selectedOption = dropdown5.options[selectedIndex].text;

        if (selectedOption == "jealous of Kings life")
        {
            
            correctCheck5 = true;


        }
        else
        {
            correctCheck5 = false;
        }
    }
    public void SubmitConclusion()
    {
        
        typewriterStuff.gameObject.SetActive(false);
        //replaced with newspaper past prototype
        if (correctCheck1 == true&& correctCheck2 == true && correctCheck3 == true && correctCheck4 == true && correctCheck5 == true)
        {
            finalCorrectCheck = true;
        }
        else
        {
            finalCorrectCheck = false;
        }
        if (finalCorrectCheck == true)
        {
            _titleFadeAway.fadein(true);
        }
        else if(finalCorrectCheck==false)
        {
            _titleFadeAway.fadein(false);
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
        if ( FirstID== EvidenceID.A01_01 || SecondID== EvidenceID.A01_01)
        {
            dropdown1.options.Add(new TMP_Dropdown.OptionData("Darling King", null));
            dropdown1.RefreshShownValue();
        }
        else if (FirstID== EvidenceID.A01_03 || SecondID == EvidenceID.A01_03)
        {
            dropdown1.options.Add(new TMP_Dropdown.OptionData("Steven Knight", null));
            dropdown1.RefreshShownValue();
        }
        else if (FirstID == EvidenceID.A01_06 || SecondID == EvidenceID.A01_06)
        {
            dropdown1.options.Add(new TMP_Dropdown.OptionData("Guioco Piano", null));
            dropdown1.RefreshShownValue();
        }
        else if (FirstID == EvidenceID.A04_02||SecondID==EvidenceID.A04_02)
        {
            dropdown5.options.Add(new TMP_Dropdown.OptionData("a rat", null));
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
            dropdown3.options.Add(new TMP_Dropdown.OptionData("The Alleyway", null));
            dropdown3.RefreshShownValue();
        }
        else if (FirstID == EvidenceID.A01_09 || SecondID == EvidenceID.A01_09)
        {
            dropdown4.options.Add(new TMP_Dropdown.OptionData("A large frame revolver", null));
            dropdown4.RefreshShownValue();
        }
        else if (FirstID == EvidenceID.A04_04 || SecondID == EvidenceID.A04_04)
        {
            dropdown5.options.Add(new TMP_Dropdown.OptionData("jealous of Kings life", null));
            dropdown5.RefreshShownValue();
        }
    }
    private void Awake()
    {
        typewriterStuff.gameObject.SetActive(false);
        _titleFadeAway = FindObjectOfType<TitleFadeAway>();
    }
}
