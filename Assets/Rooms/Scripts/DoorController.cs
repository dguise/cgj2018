using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {
	private GameObject door;
	private GameObject fence;
	private GameObject doorLight;
	private CircleCollider2D trigger;

	private enum State {
		Active,
		Finished
	}
	private State state = State.Active;

	private float timestamp;
	private float unlockTime = 1f;

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
		trigger.enabled = false;
		doorLight.SetActive(false);
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
		if (state != State.Finished) {
			doorLight.SetActive(true);
			trigger.enabled = true;
			timestamp = Time.time;
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == Tags.Player && Time.time - timestamp > unlockTime) {
			OpenDoor();
			LockDoor();
			GetComponentInParent<RoomSpawner>().LockAllRooms();
			state = State.Finished;
		}
	}
}
