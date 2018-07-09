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

    void Start ()
    {
        this.portraitImage = this.GetComponentsInChildren<UnityEngine.UI.Image>()[3];
    }
	
	void Update ()
    {
        if (Input.anyKeyDown)
        {
            this.currentChoice += 1;
            if (currentChoice == choices)
            {
                this.currentChoice = 0;
            }

            this.portraitImage.sprite = portraitsArray[this.currentChoice];
            Transform rightArrow = transform.Find("RightArrow");
            
            var anim = rightArrow.GetComponent<Animator>();
            anim.SetTrigger("RightArrow");
        }
    }
}