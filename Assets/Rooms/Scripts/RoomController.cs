using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour {
	public static class Direction {
		public const string NORTH = "DoorNorth";
		public const string EAST = "DoorEast";
		public const string SOUTH = "DoorSouth";
		public const string WEST = "DoorWest";
	}

	private Dictionary<string, Transform> doors = new Dictionary<string, Transform>();

	// Use this for initialization
	void Start () {
		doors.Add(Direction.NORTH, transform.Find(Direction.NORTH));
		doors.Add(Direction.EAST, transform.Find(Direction.EAST));
		doors.Add(Direction.SOUTH, transform.Find(Direction.SOUTH));
		doors.Add(Direction.WEST, transform.Find(Direction.WEST));

		SetAllDoors(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetDoor (bool open, string direction) {
		Transform door = doors[direction];
		door.gameObject.SetActive(!open);
	}

	public void SetAllDoors(bool open) {
		SetDoor(true, Direction.NORTH);
		SetDoor(true, Direction.EAST);
		SetDoor(true, Direction.SOUTH);
		SetDoor(true, Direction.WEST);
	}
}
