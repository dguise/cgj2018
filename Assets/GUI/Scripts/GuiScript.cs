using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiScript : MonoBehaviour {

#pragma warning disable 0414
    float lifetime = 5.0f;   //lifetime in seconds
    float rateOfText = 0.05f;  //Time between letters
    float currentRateOfText
    {
        get { return speedUpText == true ? rateOfText / 2 : rateOfText; }
    }
    bool speedUpText = false;
    bool clickToGetToNextMessageBubble = false;

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

    private Queue messageQueue = new Queue();

    string textToAdd = "";
    //////////////////////////////////////////////////////////////////////////////////////////////////////
#pragma warning restore 0414


    void Start ()
    {
        this.image = GetComponent<Image>();
        this.targetAlpha = this.image.color.a;
        myText = this.GetComponentInChildren<Text>();
        messageQueue.Enqueue("Ret 2 go!\n\nAdd some cool rp text here and profit from great fame and fortune!");
        messageQueue.Enqueue("Hello Emil, tell me about your day!");
        messageQueue.Enqueue("Jag kan inte, jag håller på att svimma!");
        messageQueue.Enqueue("Jag menar, jag håller på att implodera!");
        messageQueue.Enqueue("BAM!  Okej, sorry!");
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

        if (myText.text == string.Empty)
            FadeOut();

        bool anyAbuttonDown = speedUpText = Input.GetButtonDown(Inputs.AButton());

        if (clickToGetToNextMessageBubble)  //Vi väntar på input ifrån spelaren
            if (anyAbuttonDown)  //Spelaren vill få nästa äventyrsbubbla
                StartCoroutine(AddText());


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
        ClearText();
        textToAdd = messageQueue.Dequeue().ToString(); //Hämta första elementet i kön (och ta bort det ifrån listan)

        if (messageQueue.Count > 0) //Det kommer mer text efter detta
            textToAdd += "      ...";

        foreach (var c in textToAdd)
        {
            myText.text += c;
            yield return new WaitForSeconds(rateOfText);
        }

        if (messageQueue.Count > 0)
            clickToGetToNextMessageBubble = true;
            //StartCoroutine(AddText());
    }

    private void ClearText()
    {
        myText.text = string.Empty;
    }
}
