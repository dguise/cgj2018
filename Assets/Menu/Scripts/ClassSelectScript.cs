using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class ClassSelectScript : MonoBehaviour {

    public Sprite[] portraitsArray = new Sprite[Enum.GetNames(typeof(PlayerManager.CharacterClassesEnum)).Length];
    private int choices = Enum.GetNames(typeof(PlayerManager.CharacterClassesEnum)).Length;
    private int currentChoice = 0;
    private UnityEngine.UI.Image portraitImage;
    public int playerID = 1;
    public float timestamp;
    float cooldown = 0.3f;

    void Start ()
    {
        this.portraitImage = this.GetComponentsInChildren<UnityEngine.UI.Image>()[3];
        timestamp = -cooldown;
    }
	
	void FixedUpdate() 
    {
        if (PlayerManager.playerReady[playerID])
        {
		    float horizontal = Input.GetAxisRaw(Inputs.Horizontal(playerID));
            if (Mathf.Abs(horizontal) > 0.5 && timestamp + cooldown < Time.time)
            {
                timestamp = Time.time;
                this.currentChoice = ((this.currentChoice + (int)Mathf.Ceil(horizontal)) % choices + choices) % choices;

                this.portraitImage.sprite = portraitsArray[this.currentChoice];
                
                if (horizontal > 0) {
                    Transform rightArrow = transform.Find("RightArrow");
                    var anim = rightArrow.GetComponent<Animator>();
                    //anim.SetTrigger("RightArrow");
                } else {
                    Transform leftArrow = transform.Find("LeftArrow");
                    var anim = leftArrow.GetComponent<Animator>();
                    //anim.SetTrigger("LeftArrow");
                }

                PlayerManager.playerClass[playerID] = (PlayerManager.CharacterClassesEnum)this.currentChoice;
            }
        } else {
            if (Input.GetKeyDown(KeyCode.Space)) {
                this.portraitImage.sprite = portraitsArray[this.currentChoice];
                PlayerManager.playerReady[playerID] = true;
                //Instantiate Player
            }
        }
    }
}