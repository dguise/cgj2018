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
}
