using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiScript : MonoBehaviour {

#pragma warning disable 0414
    float lifetime = 5.0f;   //lifetime in seconds
    float waitTime = 0.05f;  //Time between letters


    //Image properties
    private float FadeRate = 2.5f;  //Rate of fade
    private Image image;
    private float targetAlpha;
    //////////////////////////////////////////////////////////////////////////////////////////////////////



    //Text properties
    Text myText;
    string currentText = string.Empty;
    private float targetAlphaText;
    enum MessagetypeEnum
    {
        Helptext,
        Infotext,
        Warning,
        Nonskippable
    }
    MessagetypeEnum messageType = MessagetypeEnum.Helptext;

    string textToAdd = "Ret 2 go!\n\nAdd some cool rp text here and profit from great fame and fortune!";
    //////////////////////////////////////////////////////////////////////////////////////////////////////
#pragma warning restore 0414


    void Start ()
    {
        this.image = GetComponent<Image>();
        this.targetAlpha = this.image.color.a;
        myText = this.GetComponentInChildren<Text>();
        StartCoroutine(AddText());
    }


    void Update()
    {
        Color curColor = this.image.color;
        float alphaDiff = Mathf.Abs(curColor.a - this.targetAlpha);
        if (alphaDiff > 0.0001f)
        {
            curColor.a = Mathf.Lerp(curColor.a, targetAlpha, this.FadeRate * Time.deltaTime);
            this.image.color = curColor;
        }
        Color curColorText = this.myText.color;
        float alphaDiffText = Mathf.Abs(curColorText.a - this.targetAlpha);
        if (alphaDiff > 0.0001f)
        {
            curColorText.a = Mathf.Lerp(curColorText.a, targetAlpha, this.FadeRate * Time.deltaTime);
            this.myText.color = curColorText;
        }
    }

    void FixedUpdate() { }
    void LateUpdate() { }

    public void FadeOut()
    {
        this.targetAlpha = 0.0f;
    }

    public void FadeIn()
    {
        this.targetAlpha = 1.0f;
    }

    IEnumerator AddText()
    {
        foreach (var c in textToAdd)
        {
            myText.text += c;
            yield return new WaitForSeconds(waitTime);
        }
        FadeOut();
    }
}
