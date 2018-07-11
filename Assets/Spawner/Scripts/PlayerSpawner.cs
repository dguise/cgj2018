using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {
	public Animator[] spawners;

	void Start () {
		spawners = GetComponentsInChildren<Animator>();
		for (int i = 0; i < PlayerManager.players; i++) {
			if (PlayerManager.playerReady[i]) {
				Spawn(i);
			}
		}
	}

	public void Animate(int id) {
		spawners[id].SetTrigger("ShouldSpawn");
	}

	public void AnimateAll() {
		foreach (var spawn in spawners) {
			spawn.SetTrigger("ShouldSpawn");
		}
	}

	public void Spawn(int id) {
		GameObject player = PlayerManager.PlayerObjects[id];
		player.transform.position = spawners[id].transform.position;
		Animate(id);
	}
}
