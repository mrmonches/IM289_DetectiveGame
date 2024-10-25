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
    [SerializeField] private float fadeInTime = 0f;
    [SerializeField] private Image newspaperCorrect;
    [SerializeField] private Image newspaperIncorrect;
    private bool fade_in = false;

    // Start is called before the first frame update
    void Awake()
    {
        newspaperCorrect.gameObject.SetActive(false);

        newspaperIncorrect.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (fade_in == false)
        {
            if (fadeTime > 0)
            {
                fadeTime -= Time.deltaTime;
                titleText.color = new Color(titleText.color.r, titleText.color.g, titleText.color.b, fadeTime);
                titleText2.color = new Color(titleText2.color.r, titleText2.color.g, titleText2.color.b, fadeTime);
                titleBackground.color = new Color(titleBackground.color.r, titleBackground.color.g, titleBackground.color.b, fadeTime);
            }
        }
    }

   public void fadein(bool correct)
    {
       while(fadeTime<3)
        {
            titleBackground.gameObject.SetActive(true);
            if(correct==true)
            {
                newspaperCorrect.gameObject.SetActive(true);
            }
            else
            {
                newspaperIncorrect.gameObject.SetActive(true);
            }
            titleBackground.color = new Color(titleBackground.color.r, titleBackground.color.g, titleBackground.color.b, fadeTime);
            fadeTime += 1;
            fade_in = true;
        }
    }

    public void disabletitle()
    {
        titleBackground.gameObject.SetActive(false);
        titleText.gameObject.SetActive(false);
        titleText2.gameObject.SetActive(false);
    }
}
