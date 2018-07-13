using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiScript : MonoBehaviour
{
    public static GuiScript instance = null;
#pragma warning disable 0414
    float lifetime = 5.0f;   //lifetime in seconds
    float rateOfText = 0.1f;  //Time between letters
    float currentRateOfText
    {
        get { return speedUpText == true ? rateOfText / 5 : rateOfText; }
    }
    bool speedUpText = false;
    bool clickToGetToNextMessageBubble = false;
    bool currentlySpammingText = false;

    //Image properties
    private float FadeRate = 2.5f;  //Rate of fade
    private Image image;
    private float targetAlpha;
    private Image leftPortraitImage;
    private Image rightPortraitImage;
    //////////////////////////////////////////////////////////////////////////////////////////////////////



    //Text properties
    Text myText;
    string currentText = string.Empty;
    private float targetAlphaText;
    

    private Queue<Message> messageQueue = new Queue<Message>();

    string textToAdd = "";
    //////////////////////////////////////////////////////////////////////////////////////////////////////
#pragma warning restore 0414
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        this.image = GetComponent<Image>();
        this.targetAlpha = this.image.color.a;
        myText = this.GetComponentInChildren<Text>();

        leftPortraitImage = transform.GetChild(1).GetComponent<Image>();
        rightPortraitImage = transform.GetChild(2).GetComponent<Image>();
        
        //messageQueue.Enqueue("Ret 2 go!\n\nAdd some cool rp text here and profit from great fame and fortune!");
        //messageQueue.Enqueue("Hello Emil, tell me about your day!");
        //messageQueue.Enqueue("Jag kan inte, jag håller på att svimma!");
        //messageQueue.Enqueue("Jag menar, jag håller på att implodera!");
        //messageQueue.Enqueue("BAM!  Okej, sorry!");
        //StartCoroutine(AddText());
        DontDestroyOnLoad(gameObject);
    }


    void Update()
    {
        Color curColor = this.image.color;
        float alphaDiff = Mathf.Abs(curColor.a - this.targetAlpha);
        if (alphaDiff > 0.0001f)
        {
            curColor.a = Mathf.Lerp(curColor.a, targetAlpha, this.FadeRate * Time.deltaTime);
            this.image.color = curColor;
            this.leftPortraitImage.color = curColor;
            this.rightPortraitImage.color = curColor;
        }
        Color curColorText = this.myText.color;
        //float alphaDiffText = Mathf.Abs(curColorText.a - this.targetAlpha);
        if (alphaDiff > 0.0001f)
        {
            curColorText.a = Mathf.Lerp(curColorText.a, targetAlpha, this.FadeRate * Time.deltaTime);
            this.myText.color = curColorText;
        }

        if (myText.text == string.Empty)
            FadeOut();


        speedUpText = Input.anyKey;

        if (!currentlySpammingText)  //Vi väntar på input ifrån spelaren
        {
            if (Input.GetButtonDown(Inputs.AButton()))  //Spelaren vill få nästa äventyrsbubbla
                if (clickToGetToNextMessageBubble)
                    StartCoroutine(AddText());
                else
                    this.ClearText();
        }
        //Debug.Log(Time.time + "speedUpText = " + speedUpText);
        //Debug.Log(Time.time + "currentRateOfText = " + currentRateOfText);
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
        if (messageQueue.Count > 0)
        {
            Message msg = (Message)messageQueue.Dequeue(); //Hämta första elementet i kön (och ta bort det ifrån listan)
            textToAdd = msg.text;
        }
        if (messageQueue.Count > 0) //Det kommer mer text efter detta
            textToAdd += "      ...";

        currentlySpammingText = true;

        foreach (var c in textToAdd)
        {
            myText.text += c;
            yield return new WaitForSeconds(currentRateOfText);
        }

        clickToGetToNextMessageBubble = (messageQueue.Count > 0);

        currentlySpammingText = false;
    }

    private void ClearText()
    {
        myText.text = string.Empty;
    }


    private void SetPortraits(Sprite aLeftPortrait = null, Sprite aRightPortrait = null)
    {
        leftPortraitImage.sprite = aLeftPortrait;
        rightPortraitImage.sprite = aRightPortrait;

        leftPortraitImage.gameObject.SetActive(aLeftPortrait != null);
        rightPortraitImage.gameObject.SetActive(aRightPortrait != null);
    }

    public void Talk(Message msg)
    {
        messageQueue.Enqueue(msg);
    }
}

public class Message
{
    public Sprite LeftPortraitSprite = null;
    public Sprite RightPortraitSprite = null;
    public string text = string.Empty;

    public Message(Sprite aLeftPortrait = null, Sprite aRightPortrait = null, string aText = "")
    {
        LeftPortraitSprite = aLeftPortrait;
        RightPortraitSprite = aRightPortrait;
        text = aText;
    }

    public enum MessagetypeEnum
    {
        ClickToAdvance,
        Destroy2secondsAfterFinish
    }
    public MessagetypeEnum messageType = MessagetypeEnum.Destroy2secondsAfterFinish;
}
