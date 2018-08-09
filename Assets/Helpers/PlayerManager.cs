using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public static class PlayerManager
{

    public static List<GameObject> PlayerObjects = new List<GameObject>();
    public static int players { get { return PlayerObjects.Count; } }
    public static List<GameObject> PlayersAlive { get { return PlayerObjects.Where(x => !x.GetComponent<Unit>().IsDead).ToList(); } }

    public static bool gameStarted = false;
    private static bool hasSwitched = false;
    public static float time = 0f;


    public enum CharacterClasses
    {
        Melee,
        Bowman,
        Magician,
        Dartblower
    }


    public static void Reset()
    {
        PlayerObjects = new List<GameObject>();
        hasSwitched = false;
        gameStarted = false;
        time = 0f;

        for (int i = 0; i < 3; i++)
        {
            GamePad.SetVibration((PlayerIndex)i, 0, 0);
        }
    }

    public static Ability GetAbility(CharacterClasses playerClass, GameObject go)
    {
        Ability ability = new DashAbility(go);

        switch (playerClass)
        {
            case CharacterClasses.Melee:
                ability = new DashAbility(go);
                break;
            case CharacterClasses.Bowman:
                ability = new InvisibilityAbility(go);
                break;
            case CharacterClasses.Magician:
                ability = new SiphonAoeBlood(go);
                break;
            case CharacterClasses.Dartblower:
                ability = new ImmolationAbility(go);
                break;
            default:
                break;
        }

        return ability;
    }


    public static Weapon GetWeapon(CharacterClasses playerClass, GameObject go)
    {
        Weapon weapon = new Gun(go);

        switch (playerClass)
        {
            case CharacterClasses.Melee:
                weapon = new PlayerMeleeGun(go);
                break;
            case CharacterClasses.Bowman:
                weapon = new SpecialGun(go);
                break;
            case CharacterClasses.Magician:
                weapon = new ShieldGun(go);
                break;
            case CharacterClasses.Dartblower:
                weapon = new Gun(go);
                break;
            default:
                weapon = new Gun(go);
                break;
        }
        return weapon;
    }

    public static void Capricious()
    {
        if (players == 2)
        {
            var playerOne = PlayerObjects[0].GetComponent<Player>();
            var playerTwo = PlayerObjects[1].GetComponent<Player>();
            Sprite p1 = playerOne.Portrait;
            Sprite p2 = playerTwo.Portrait;

            PlayerIndex tempController = playerOne.playerIndex;
            playerOne.playerIndex = playerTwo.playerIndex;
            playerTwo.playerIndex = tempController;

            GuiScript.instance.Talk(new Message(p1, p2, "I don't feel so good...", aMessageType: Message.MessagetypeEnum.QuickMessageAllAtOnce));
            GuiScript.instance.Talk(new Message(p2, p1, "Neither do ... ", aMessageType: Message.MessagetypeEnum.QuickMessageAllAtOnce));
            GuiScript.instance.Talk(new Message(p1, p2, "... I...", aMessageType: Message.MessagetypeEnum.QuickMessageAllAtOnce));
            GuiScript.instance.Talk(new Message(p2, p1, "Wow, capricious!", aMessageType: Message.MessagetypeEnum.QuickMessageAllAtOnce));

            ParticleSpawner.instance.SpawnParticleEffect(playerOne.transform.position, ParticleTypes.BlueGlitter_OverTime);
            ParticleSpawner.instance.SpawnParticleEffect(playerTwo.transform.position, ParticleTypes.BlueGlitter_OverTime);
        }
        else if (players == 1)
        {
            Sprite p1 = PlayerObjects[0].GetComponent<Player>().Portrait;

            int length = Enum.GetNames(typeof(CharacterClasses)).Length;

            // Give player a random class & retrieve weapon/ability for it
            Player playerScript = PlayerObjects[0].GetComponent<Player>();
            playerScript.PlayerClass = (CharacterClasses)UnityEngine.Random.Range(0, length-1);
            playerScript.weapon = GetWeapon(playerScript.PlayerClass, PlayerObjects[0]);
            playerScript.ability = GetAbility(playerScript.PlayerClass, PlayerObjects[0]);

            GuiScript.instance.Talk(new Message(p1, aText: "Capricious...", aMessageType: Message.MessagetypeEnum.QuickMessageAllAtOnce));
            GuiScript.instance.Talk(new Message(p1, aText: "Capricious?", aMessageType: Message.MessagetypeEnum.QuickMessageAllAtOnce));
            GuiScript.instance.Talk(new Message(p1, aText: "Capricious!", aMessageType: Message.MessagetypeEnum.QuickMessageAllAtOnce));

            ParticleSpawner.instance.SpawnParticleEffect(playerScript.transform.position, ParticleTypes.BlueGlitter_OverTime);
        }
    }
}

public static class LevelManager
{
    public static int TempleFloor = 1;
}
