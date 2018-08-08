using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class PlayerSelection : MonoBehaviour {
    public PlayerIndex index = PlayerIndex.One;

    GamePadState state;
    GamePadState prevState;

    public GameObject[] roster;
    private Sprite[] portraitsArray;
    private UnityEngine.UI.Image portraitImage;

    private int choices = Enum.GetNames(typeof(PlayerManager.CharacterClassesEnum)).Length;
    private int currentChoice = 0;

    PlayerSpawner spawner;

    GameObject spawnedPlayer = null;

    void Start () {
        portraitsArray = roster.Select(x => x.GetComponent<Unit>().Portrait).ToArray();
        this.portraitImage = this.GetComponentsInChildren<UnityEngine.UI.Image>()[4];
        spawner = GameObject.Find("PlayerSpawner").GetComponent<PlayerSpawner>();
    }

    void Update () {
        state = GamePad.GetState(index);

        if (state.DPad.Left.IsDown() && prevState.DPad.Left.IsUp())
        {
            ChangePortrait(--currentChoice);
        }
        else if (state.DPad.Right.IsDown() && prevState.DPad.Right.IsUp())
        {
            ChangePortrait(++currentChoice);
        }
        else if (state.Buttons.A.IsDown() && prevState.Buttons.A.IsUp())
        {
            SpawnPlayer();
        }

        prevState = state;
	}

    void ChangePortrait(int nextChoice)
    {
        this.currentChoice = ((nextChoice) % choices + choices) % choices;
        this.portraitImage.sprite = portraitsArray[this.currentChoice];
        Vector3 newScale = this.portraitImage.transform.localScale;
        newScale.x = 1.5f;
        newScale.y = 2.5f;
        this.portraitImage.gameObject.transform.localScale = newScale;
    }

    void SpawnPlayer()
    {
        if (spawnedPlayer != null)
        {
            PlayerManager.PlayerObjects.Remove(spawnedPlayer);
            Destroy(spawnedPlayer);
        }

        spawnedPlayer = Instantiate<GameObject>(roster[currentChoice], spawner.spawners[(int)index].transform.position, roster[currentChoice].transform.rotation);
        spawnedPlayer.GetComponent<Player>().playerID = index;

        PlayerManager.PlayerObjects.Add(spawnedPlayer);
        spawner.Spawn((int)index, spawnedPlayer);
    }
}
