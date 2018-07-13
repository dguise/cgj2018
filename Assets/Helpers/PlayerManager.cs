using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerManager {

    public static Dictionary<int, int> controllerId = new Dictionary<int, int>();
    public static List<int> controllers = new List<int>(); 
    public static List<GameObject> PlayerObjects = new List<GameObject>();
    private static bool hasSwitched = false;

	public enum CharacterClassesEnum
    {
        Emil,
        Melee,
        Bowman,
        Magician,
        Roger,
        Dartblower
    }

    public static CharacterClassesEnum[] playerClass = {CharacterClassesEnum.Emil, CharacterClassesEnum.Emil};
    public static int players = 0;
    public static bool[] playerReady = {false, false};
    public static int playersReady = 0;



    public static Weapon GetWeapon(CharacterClassesEnum playerClass, GameObject go)
    {
        Weapon weapon = new Gun(go);

        switch (playerClass)
        {
            case CharacterClassesEnum.Emil:
                weapon = new SpecialGun(go);
                break;
            case CharacterClassesEnum.Melee:
                weapon = new MeleeGun(go);
                break;
            case CharacterClassesEnum.Bowman:
                weapon = new Gun(go);
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
                }
            }

            if (Input.GetKeyDown("space"))
            {
                controllerId[players] = -1;
                controllers.Add(-1);
                playerReady[players] = true;
                players += 1;
                playersReady += 1;
            }
        }
    }

    public static void Capricious() {
        if (players == 2) {
            Debug.Log("Test");
            //PlaySomeTextEvent
            int tempController = controllerId[0];
            controllerId[0] = controllerId[1];
            controllerId[1] = tempController;
            bool tempReady = playerReady[0];
            playerReady[0] = playerReady[1];
            playerReady[1] = tempReady;

        } else if (players == 1) {
            //PlaySomeTextEvent
            int tempClass = (int)playerClass[0];
            tempClass += 2 * (hasSwitched ? 1 : 0) - 1; 
            Debug.Log(tempClass);
            int length = CharacterClassesEnum.GetNames(typeof(PlayerManager.CharacterClassesEnum)).Length;
            playerClass[0] = (CharacterClassesEnum)(((tempClass % length) + length) % length); // hardcoded player 1, could easily be changed later
            Player playerScript = PlayerObjects[0].GetComponent<Player>();
            playerScript.PlayerClass = playerClass[0];
            playerScript.weapon = PlayerManager.GetWeapon(playerScript.PlayerClass, PlayerObjects[0]);
            playerScript.UpdateMask(playerClass[0]);
        }
    }
}
