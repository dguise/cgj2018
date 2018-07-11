using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerManager {

    public static int players {get; set;}
    public static Dictionary<int, int> controllerId;
    public static HashSet<int> controllers; 

    static PlayerManager() {
        players = 0;
        controllerId =  new Dictionary<int, int>();
        controllers = new HashSet<int>();
    }

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
    public static bool[] playerReady = {false, false};



    public static Weapon GetWeapon(CharacterClassesEnum playerClass, GameObject go)
    {
        Weapon weapon = new Gun(go);

        switch (playerClass)
        {
            case CharacterClassesEnum.Emil:
                weapon = new SpecialGun(go);
                break;
            case CharacterClassesEnum.Melee:
                weapon = new Gun(go);
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
                }
            }
        }
    }
}
