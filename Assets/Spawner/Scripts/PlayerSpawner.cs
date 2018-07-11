using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {
	public Animator[] spawners;

	void Start () {
		spawners = GetComponentsInChildren<Animator>();
	}

	public void Animate(int id) {
		spawners[id].SetTrigger("ShouldSpawn");
	}

	void AnimateAll() {
		foreach (var spawn in spawners) {
			spawn.SetTrigger("ShouldSpawn");
		}
	}
}
