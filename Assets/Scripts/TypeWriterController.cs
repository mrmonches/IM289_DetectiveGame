using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeWriterController : MonoBehaviour
{
    private int option1;
    private int option2;
    private int option3;
     
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Dropdown1(int val)
    {
        option1 = val;
    }
    public void Dropdown2(int val)
    {
        option2 = val;
    }
    public void Dropdown3(int val)
    {
        option3 = val;
    }
}
