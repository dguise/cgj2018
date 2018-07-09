using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour {
	public int nTiles = 7;
	private float origTileSize = 2;
	public float tileSize = 2;
	public float wallThiccness = 0.1f;
	private List<GameObject> roomList = new List<GameObject>();
	private GameObject[,] rooms;

	// Use this for initialization
	void Start () {
		rooms = new GameObject[nTiles, nTiles];

		// Fetch all room prefabs
		Object[] subListObjects = Resources.LoadAll("Prefabs", typeof(GameObject));
		foreach (GameObject subListObject in subListObjects) {
			GameObject lo = (GameObject)subListObject;
			roomList.Add(lo);
		}

		// Instantiate rooms
		float scale = tileSize / origTileSize;
		for(int x = -(nTiles - 1) / 2; x <= (nTiles - 1) / 2; x++) {
			for(int y = -(nTiles - 1) / 2; y <= (nTiles - 1) / 2; y++) {
				Vector3 v = new Vector3(x * (tileSize - scale*wallThiccness), y * (tileSize - scale*wallThiccness), 0);
				int r = Random.Range(0, roomList.Count);
				int xroom = x + (nTiles - 1) / 2;
				int yroom = y + (nTiles - 1) / 2;
				rooms[xroom, yroom] = Object.Instantiate(roomList[r], v, Quaternion.identity, transform);
				rooms[xroom, yroom].transform.localScale = new Vector3(scale, scale, 1f);
			}
		}

		for (int i = 0; i < nTiles; i++) {
			rooms[0, i].GetComponent<RoomController>().SetDoor(false, "DoorWest");
			rooms[nTiles-1, i].GetComponent<RoomController>().SetDoor(false, "DoorEast");
			rooms[i, 0].GetComponent<RoomController>().SetDoor(false, "DoorSouth");
			rooms[i, nTiles-1].GetComponent<RoomController>().SetDoor(false, "DoorNorth");
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
