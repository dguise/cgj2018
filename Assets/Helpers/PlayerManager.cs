﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class PlayerManager {

    public static Dictionary<int, int> controllerId = new Dictionary<int, int>();
    public static List<int> controllers = new List<int>(); 
    public static List<GameObject> PlayerObjects = new List<GameObject>();
    private static bool hasSwitched = false;
    public static CharacterClassesEnum[] playerClass = {CharacterClassesEnum.Magician, CharacterClassesEnum.Magician};
    public static int players = 0;
    public static bool[] playerReady = {false, false};
    public static int playersReady = 0;
    public static bool gameStarted = false;
    public static float time = 0f;


	public enum CharacterClassesEnum
    {
        Melee,
        Bowman,
        Magician,
        Roger,
        Dartblower
    }

    public static void Reset() {
        controllerId = new Dictionary<int, int>();
        controllers = new List<int>(); 
        PlayerObjects = new List<GameObject>();
        hasSwitched = false;
        playerClass = new CharacterClassesEnum[] {CharacterClassesEnum.Magician, CharacterClassesEnum.Magician};
        players = 0;
        playerReady = new bool[] {false, false};
        playersReady = 0;
        gameStarted = false;
        time = 0f;
    }



    public static Weapon GetWeapon(CharacterClassesEnum playerClass, GameObject go)
    {
        Weapon weapon = new Gun(go);

        switch (playerClass)
        {
            case CharacterClassesEnum.Melee:
                weapon = new PlayerMeleeGun(go);
                break;
            case CharacterClassesEnum.Bowman:
                weapon = new SpecialGun(go);
                break;
            case CharacterClassesEnum.Magician:
                weapon = new ShieldGun(go);
                break;
            case CharacterClassesEnum.Roger:
                weapon = new Gun(go);
                break;
            case CharacterClassesEnum.Dartblower:
                weapon = new Gun(go);
                break;
            default:
                weapon = new Gun(go);
                break;
        }
        return weapon;
    }

    // Remove for production?
    public static void MapControllerToPlayer() 
    {
        if (players < 2) {
            for (int i = 1; i <= 16; i++) {
                if (Input.GetButton(Inputs.AButton(i)) && !PlayerManager.controllers.Contains(i)) {
                    controllerId[players] = i;
                    controllers.Add(i);
                    playerReady[players] = true;
                    players += 1;
                    playersReady += 1;
                    Debug.LogWarning("REMOVE THIS FOR PRODUCTION VVVV");
                    PlayerManager.PlayerObjects.Add(GameObject.FindGameObjectWithTag(Tags.Player));
                    // End of remove
                }
            }

            if (Input.GetKeyDown("space"))
            {
                controllerId[players] = -1;
                controllers.Add(-1);
                playerReady[players] = true;
                players += 1;
                playersReady += 1;
                Debug.LogWarning("REMOVE THIS FOR PRODUCTION VVVV");
                PlayerManager.PlayerObjects.Add(GameObject.FindGameObjectWithTag(Tags.Player));
                // End of remove
            }
        }
    }

    public static void Capricious() {
        if (players == 2) {
            Sprite p1 = PlayerObjects[0].GetComponent<Player>().Portrait;
            Sprite p2 = PlayerObjects[1].GetComponent<Player>().Portrait;
            GuiScript.instance.Talk(new Message(p1, p2, "I don't feel so good...", aMessageType: Message.MessagetypeEnum.QuickMessageAllAtOnce));
            GuiScript.instance.Talk(new Message(p2, p1, "Neither do ... ", aMessageType: Message.MessagetypeEnum.QuickMessageAllAtOnce));
            GuiScript.instance.Talk(new Message(p1, p2, "... I.", aMessageType: Message.MessagetypeEnum.QuickMessageAllAtOnce));

            int tempController = controllerId[0];
            controllerId[0] = controllerId[1];
            controllerId[1] = tempController;
        } else if (players == 1) {
            Sprite p1 = PlayerObjects[0].GetComponent<Player>().Portrait;
            GuiScript.instance.Talk(new Message(p1, aText: "Capricious...", aMessageType: Message.MessagetypeEnum.QuickMessageAllAtOnce));
            GuiScript.instance.Talk(new Message(p1, aText: "Capricious?", aMessageType: Message.MessagetypeEnum.QuickMessageAllAtOnce));
            GuiScript.instance.Talk(new Message(p1, aText: "Capricious!", aMessageType: Message.MessagetypeEnum.QuickMessageAllAtOnce));
            int tempClass = (int)playerClass[0];
            tempClass += 2 * (hasSwitched ? 1 : 0) - 1; 
            hasSwitched = !hasSwitched;
            int length = CharacterClassesEnum.GetNames(typeof(PlayerManager.CharacterClassesEnum)).Length;
            playerClass[0] = (CharacterClassesEnum)(((tempClass % length) + length) % length); // hardcoded player 1, could easily be changed later
            Player playerScript = PlayerObjects[0].GetComponent<Player>();
            playerScript.PlayerClass = playerClass[0];
            playerScript.weapon = PlayerManager.GetWeapon(playerScript.PlayerClass, PlayerObjects[0]);
            playerScript.UpdateMask(playerClass[0]);
        }
    }
}
