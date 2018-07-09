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

    public static CharacterClassesEnum playerOneClass = CharacterClassesEnum.Emil;
    public static CharacterClassesEnum playerTwoClass = CharacterClassesEnum.Emil;

    public static bool playerOneReady = false;
    public static bool playerTwoReady = false;


}
