using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleFadeAway : MonoBehaviour
{
    [SerializeField] private float fadeTime = 3f;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI titleText2;
    [SerializeField] private Image titleBackground;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeTime>0)
        {
            fadeTime -= Time.deltaTime;
            titleText.color = new Color(titleText.color.r, titleText.color.g, titleText.color.b, fadeTime);
            titleText2.color = new Color(titleText2.color.r, titleText2.color.g, titleText2.color.b, fadeTime);
            titleBackground.color = new Color(titleBackground.color.r, titleBackground.color.g, titleBackground.color.b, fadeTime);
        }
    }
}
