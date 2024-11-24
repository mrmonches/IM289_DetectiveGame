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
    private bool correctCheck1 = false;
    private bool correctCheck2 = false;
    private bool correctCheck3 = false;
    private bool correctCheck4 = false;
    private bool correctCheck5 = false;
    private bool finalCorrectCheck = false;
    [SerializeField] private EvidenceData _evidenceData;
    [SerializeField] private GameObject typewriterStuff;
    //This is used to prevent moving between stations when in the typewriter 
    [SerializeField] private GameObject _cameraController;
    private TitleFadeAway _titleFadeAway;
    private PlayerController _playerController;

    private AudioSource _audioSource;

    private bool isActive;

    [SerializeField] private AudioClip TypewriterClip;
    //Keep all the lists in the same order as put below the code will not work properly if all the indexes are not in the correct order
    //EvidenceID.A01_01, EvidenceID.A01_03, EvidenceID.A01_06, EvidenceID.A01_17, EvidenceID.A04_05,EvidenceID.A02_00, EvidenceID.A01_09, EvidenceID.A04_02, EvidenceID.A03_02, EvidenceID.A03_01, EvidenceID.A05_04
    [SerializeField] private List<EvidenceID> typewriterID = new List<EvidenceID>();
    //"Darling King", "Steven Knight", "Guioco Piano", "11:22PM", "The Red Stiletto", "The Alleyway", "A large frame revolver", "a rat","Wanted a personal chest from King","Wanted their inheritance from King","Jealous of Kings life"
    [SerializeField] private List<string> typewriterText = new List<string>();
    //1,1,1,3,2,2,4,5,5,5,5
    [SerializeField] private List<int> typewriterNumber = new List<int>();


    /// <summary>
    /// Changed this to new input system and moved it's activation to the PlayerController
    /// </summary>
    private void ShowCanvas()
    {
        _cameraController.GetComponent<CameraController>().getCannotMove();
        typewriterStuff.gameObject.SetActive(true);
        _playerController.updatePaperOpen(true,"TypeWriter");

        isActive = true;
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
            correctCheck2 = true;
           



        }
        else
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
        else
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

        if (selectedOption == "Jealous of Kings life")
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
        _playerController.updatePaperOpen(false,"null");

        isActive = false;
    }
    public void CorrectOption(EvidenceID FirstID,EvidenceData SecondData)
    {

        
      
        EvidenceID SecondID = SecondData.EvidenceID;

        for(int i=0;i<typewriterID.Count;i++)
        {

            if (FirstID == typewriterID[i] || SecondID == typewriterID[i])
            {
                if (typewriterNumber[i]==1)
                {
                    dropdown1.options.Add(new TMP_Dropdown.OptionData(typewriterText[i], null));
                    typewriterID.RemoveAt(i);
                    typewriterText.RemoveAt(i);
                    typewriterNumber.RemoveAt(i);
                    dropdown1.RefreshShownValue();
                }
                else if (typewriterNumber[i] == 2)
                {
                    dropdown2.options.Add(new TMP_Dropdown.OptionData(typewriterText[i], null));
                    typewriterID.RemoveAt(i);
                    typewriterText.RemoveAt(i);
                    typewriterNumber.RemoveAt(i);
                    dropdown2.RefreshShownValue();
                }
                else if (typewriterNumber[i] == 3)
                {
                    dropdown3.options.Add(new TMP_Dropdown.OptionData(typewriterText[i], null));
                    typewriterID.RemoveAt(i);
                    typewriterText.RemoveAt(i);
                    typewriterNumber.RemoveAt(i);
                    dropdown3.RefreshShownValue();
                }
                else if (typewriterNumber[i] == 4)
                {
                    dropdown4.options.Add(new TMP_Dropdown.OptionData(typewriterText[i], null));
                    typewriterID.RemoveAt(i);
                    typewriterText.RemoveAt(i);
                    typewriterNumber.RemoveAt(i);
                    dropdown4.RefreshShownValue();
                }
                else
                {
                    dropdown5.options.Add(new TMP_Dropdown.OptionData(typewriterText[i], null));
                    typewriterID.RemoveAt(i);
                    typewriterText.RemoveAt(i);

                    typewriterNumber.RemoveAt(i);
                    dropdown5.RefreshShownValue();
                }
            }
        }
    }
    private void Awake()
    {
        typewriterStuff.gameObject.SetActive(false);
        _titleFadeAway = FindObjectOfType<TitleFadeAway>();
        _playerController = FindObjectOfType <PlayerController>();
    }

    public bool GetTypewriterStatus()
    {
        return isActive;
    }
}

