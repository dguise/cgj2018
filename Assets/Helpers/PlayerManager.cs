using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerManager {

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
                weapon = new Gun(go);
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
}
