using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour {
	public static class Direction {
		public const string NORTH = "North/";
		public const string EAST = "East/";
		public const string SOUTH = "South/";
		public const string WEST = "West/";
	}

	private Dictionary<string, Transform> doors = new Dictionary<string, Transform>();
	private Light light;
	public float lightTriggerRadius = 15f;
	public float lightMaxRadius = 2f;
	public float maxLight = 30;

	void Awake () {
		doors.Add(Direction.NORTH + "Door", transform.Find(Direction.NORTH + "Door"));
		doors.Add(Direction.EAST + "Door", transform.Find(Direction.EAST + "Door"));
		doors.Add(Direction.SOUTH + "Door", transform.Find(Direction.SOUTH + "Door"));
		doors.Add(Direction.WEST + "Door", transform.Find(Direction.WEST + "Door"));

		SetAllDoors(true);

		light = transform.Find("Light").gameObject.GetComponent<Light>();
	}

	void Update() {
		if (Vector2.Distance(PlayerManager.PlayerObjects[0].transform.position, light.transform.position) < lightTriggerRadius) {
			light.gameObject.SetActive(true);
			float normdist = Mathf.Max(Vector2.Distance(PlayerManager.PlayerObjects[0].transform.position, light.transform.position) - lightMaxRadius, 0);
			float factor = 1 - normdist / (lightTriggerRadius - lightMaxRadius);
			light.range = maxLight * factor;
		} else {
			light.gameObject.SetActive(false);
		}

		// TODO: Fix for both players
	}

	public void SetDoor (bool open, string direction) {
		Transform door = doors[direction + "Door"];
		Transform fence = transform.Find(direction + "fence");
		door.gameObject.SetActive(!open);
		fence.gameObject.SetActive(!open);
	}

	public void SetAllDoors(bool open) {
		SetDoor(open, Direction.NORTH);
		SetDoor(open, Direction.EAST);
		SetDoor(open, Direction.SOUTH);
		SetDoor(open, Direction.WEST);
	}
}