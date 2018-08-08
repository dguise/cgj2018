using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {
	public Animator[] spawners;

	void Start () {
		spawners = GetComponentsInChildren<Animator>();

        int i = 0;
        foreach (var player in PlayerManager.PlayerObjects)
            Spawn(i++, player);
	}

	public void Animate(int id) {
		spawners[id].SetTrigger("ShouldSpawn");
	}

	public void AnimateAll() {
		foreach (var spawn in spawners) {
			spawn.SetTrigger("ShouldSpawn");
		}
	}

	public void Spawn(int id, GameObject player) {
		player.transform.position = spawners[id].transform.position;
		Animate(id);
	}
}
