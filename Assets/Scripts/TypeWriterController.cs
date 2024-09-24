using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriterController : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropdown1;
    [SerializeField] TMP_Dropdown dropdown2;
    [SerializeField] TMP_Dropdown dropdown3;
    [SerializeField] GameObject typewriter;
    [SerializeField] Button submit;
    [SerializeField] Button backToDesk;
    [SerializeField] TMP_Text winLoseText;
    private int option1;
    private int option2;
    private int option3;
    private bool correctCheck1=false;
    private bool correctCheck2=false;
    private bool correctCheck3=false;
    private bool finalCorrectCheck=false;
    [SerializeField] private EvidenceData _evidenceData;
    //Create a List of new Dropdown options


    // Start is called before the first frame update
    void Start()
    {
        dropdown1.gameObject.SetActive(false);
        dropdown2.gameObject.SetActive(false);
        dropdown3.gameObject.SetActive(false);
        submit.gameObject.SetActive(false);
        backToDesk.gameObject.SetActive(false);
        winLoseText.gameObject.SetActive(false);
    }
    private void OnMouseDown()
    {
        dropdown1.gameObject.SetActive(true);
        dropdown2.gameObject.SetActive(true);
        dropdown3.gameObject.SetActive(true);
        submit.gameObject.SetActive(true);
        backToDesk.gameObject.SetActive(true);

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
        dropdown1.gameObject.SetActive(false);
        dropdown2.gameObject.SetActive(false);
        dropdown3.gameObject.SetActive(false);
        submit.gameObject.SetActive(false);
        backToDesk.gameObject.SetActive(false);
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
        dropdown1.gameObject.SetActive(false);
        dropdown2.gameObject.SetActive(false);
        dropdown3.gameObject.SetActive(false);
        submit.gameObject.SetActive(false);
        backToDesk.gameObject.SetActive(false);
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
}
