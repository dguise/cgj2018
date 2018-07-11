using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {
	Animator[] spawners;

	void Start () {
		spawners = GetComponentsInChildren<Animator>();
		foreach (var spawn in spawners) {
			spawn.SetTrigger("ShouldSpawn");
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
	}
}
