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
    public bool released;
    PlayerSpawner spawner;

    float cooldown = 0.3f;
    GameObject[] players = new GameObject[2];

    void Start ()
    {
        this.portraitImage = this.GetComponentsInChildren<UnityEngine.UI.Image>()[4];
        spawner = GameObject.Find("PlayerSpawner").GetComponent<PlayerSpawner>();
        released = true;
    }
	
	void FixedUpdate() 
    {
        if (PlayerManager.playerReady[playerID])
        {
		    float horizontal = Input.GetAxisRaw(Inputs.DPadAxis(PlayerManager.controllerId[playerID]));
            if (Mathf.Abs(horizontal) > 0.5 && released)
            {
                released = false;
                this.currentChoice = ((this.currentChoice + (int)Mathf.Ceil(horizontal)) % choices + choices) % choices;

                if (portraitsArray.Length - 1 >= this.currentChoice)
                    this.portraitImage.sprite = portraitsArray[this.currentChoice];
                
                if (horizontal > 0) {
                    Transform rightArrow = transform.Find("RightArrow");
                    var anim = rightArrow.GetComponent<Animator>();
                    anim.SetTrigger("RightArrow");
                } else {
                    Transform leftArrow = transform.Find("LeftArrow");
                    var anim = leftArrow.GetComponent<Animator>();
                    anim.SetTrigger("LeftArrow");
                }

                PlayerManager.playerClass[playerID] = (PlayerManager.CharacterClassesEnum)this.currentChoice;
                Player playerScript = players[playerID].GetComponent<Player>();
                playerScript.PlayerClass = (PlayerManager.CharacterClassesEnum)this.currentChoice;
                playerScript.weapon = PlayerManager.GetWeapon(playerScript.PlayerClass, players[playerID]);
                playerScript.UpdateMask(playerScript.PlayerClass);
                //Debug.Log(players[playerID].GetComponent<Player>().PlayerClass);

            } else if (Mathf.Abs(horizontal) < 0.5 && !released) 
            {
                released = true;
            }
        } else {
            if (PlayerManager.players == playerID) {
                for (int i = 1; i <= 16; i++) {
                    if (Input.GetButton(Inputs.AButton(i)) && !PlayerManager.controllers.Contains(i)) {
                        this.portraitImage.sprite = portraitsArray[this.currentChoice];
                        PlayerManager.controllerId[playerID] = i;
                        PlayerManager.controllers.Add(i);
                        PlayerManager.playerReady[playerID] = true;
                        PlayerManager.players += 1;
                        PlayerManager.playersReady += 1;

                        //Instantiate Player
                        GameObject player = Resources.Load<GameObject>("Player3D");
                        players[playerID] = Instantiate(player, spawner.spawners[playerID].transform.position, player.transform.rotation);
                        PlayerManager.PlayerObjects.Add(players[playerID]);

                        spawner.Animate(playerID);
                        players[playerID].name = "Player" + playerID;
                        players[playerID].GetComponent<Player>().playerID = playerID;
                        break;
                    }
                }
            }
        }
    }
}