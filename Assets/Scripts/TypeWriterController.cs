using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private bool correctCheck;
     
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
       
        if (val==1)
        {
            correctCheck = true;
            
            
        }
        else if(val==2||val==0)
        {
            correctCheck = false;
            
        }
    }
    public void Dropdown2(int val)
    {
        if (val == 1)
        {
            correctCheck = true;

        }
        else if (val == 0||val==2)
        {
            correctCheck = false;
           
        }
    }
    public void Dropdown3(int val)
    {
        if (val == 2)
        {
            correctCheck = true;
            
        }
        else if(val==1||val==0)
        {
            correctCheck = false;
            
        }
    }
    public void SubmitConclusion()
    {
        dropdown1.gameObject.SetActive(false);
        dropdown2.gameObject.SetActive(false);
        dropdown3.gameObject.SetActive(false);
        submit.gameObject.SetActive(false);
        backToDesk.gameObject.SetActive(false);
        //replaced with newspaper past prototype
        if (correctCheck == true)
        {
            winLoseText.text = "You locked the criminal behind bars!";
        }
        else if(correctCheck==false)
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

}
