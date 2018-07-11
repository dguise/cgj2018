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

	void Awake () {
		doors.Add(Direction.NORTH + "Door", transform.Find(Direction.NORTH + "Door"));
		doors.Add(Direction.EAST + "Door", transform.Find(Direction.EAST + "Door"));
		doors.Add(Direction.SOUTH + "Door", transform.Find(Direction.SOUTH + "Door"));
		doors.Add(Direction.WEST + "Door", transform.Find(Direction.WEST + "Door"));

		SetAllDoors(false);
	}

	public void SetDoor (bool open, string direction) {
		Debug.Log(direction + "fence");
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
