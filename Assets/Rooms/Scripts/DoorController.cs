using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {
	private GameObject door;
	private GameObject fence;
	private GameObject doorLight;
	private CircleCollider2D trigger;

	// Use this for initialization
	void Awake () {
		door = transform.Find("Door").gameObject;
		fence = transform.Find("fence").gameObject;
		doorLight = transform.Find("DoorLight").gameObject;
		trigger = GetComponent<CircleCollider2D>();
		CloseDoor();
		LockDoor();
	}

	public void OpenDoor() {
		door.SetActive(false);
		fence.SetActive(false);
	}

	public void CloseDoor() {
		door.SetActive(true);
		fence.SetActive(true);
	}

	public void LockDoor() {
		doorLight.SetActive(false);
		trigger.enabled = false;
	}

	public void UnlockDoor() {
		doorLight.SetActive(true);
		trigger.enabled = true;
	}

	void OnTriggerEnter2D(Collider2D col) {
		OpenDoor();
		LockDoor();
	}
}
