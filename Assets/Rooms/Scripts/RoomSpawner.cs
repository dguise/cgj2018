using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour {
	public int nTiles = 7;
	//private float origTileSize = 2;
	public float xTileSize = 12;
	public float yTileSize = 12;
	public float wallThiccness = 0.1f;
	public float scale = 6;
	private List<GameObject> roomList = new List<GameObject>();
	private GameObject[,] rooms;

	// Use this for initialization
	void Start () {
		nTiles = 5 - (LevelManager.TempleFloor - 1) * 2;

		rooms = new GameObject[nTiles, nTiles];

		// Fetch all room prefabs
		Object[] subListObjects = Resources.LoadAll("Regular", typeof(GameObject));
		foreach (GameObject subListObject in subListObjects) {
			GameObject lo = (GameObject)subListObject;
			roomList.Add(lo);
		}

		// Instantiate rooms
		Object[] bossRooms = Resources.LoadAll("Boss", typeof(GameObject));
		GameObject bossRoom = (GameObject) bossRooms[0];
		rooms[(nTiles - 1) / 2, (nTiles - 1) / 2] = Object.Instantiate(bossRoom, Vector3.zero, Quaternion.identity, transform);
		rooms[(nTiles - 1) / 2, (nTiles - 1) / 2].transform.localScale = new Vector3(scale, scale, 1f);
		rooms[(nTiles - 1) / 2, (nTiles - 1) / 2].GetComponent<RoomController>().SetLevel(1f);

		for(int x = -(nTiles - 1) / 2; x <= (nTiles - 1) / 2; x++) {
			for(int y = -(nTiles - 1) / 2; y <= (nTiles - 1) / 2; y++) {
				if (x == 0 && y == 0) // Skip boss room
					continue;

				Vector3 v = new Vector3(x * xTileSize, y * yTileSize, 0);
				int xroom = x + (nTiles - 1) / 2;
				int yroom = y + (nTiles - 1) / 2;
				float level = 1f - (float) Mathf.Max(Mathf.Abs(x), Mathf.Abs(y)) / ((nTiles - 1) / 2);
				int r = Random.Range(0, roomList.Count);

				GameObject room = roomList[r];
				rooms[xroom, yroom] = Object.Instantiate(room, v, Quaternion.identity, transform);
				rooms[xroom, yroom].transform.localScale = new Vector3(scale, scale, 1f);
				rooms[xroom, yroom].GetComponent<RoomController>().SetLevel(level);
			}
		}

		for (int i = 0; i < nTiles; i++) {
			rooms[0, i].GetComponent<RoomController>().setWestWallActive(true);
			rooms[nTiles-1, i].GetComponent<RoomController>().setEastWallActive(true);
			rooms[i, 0].GetComponent<RoomController>().setSouthWallActive(true);
			rooms[i, nTiles-1].GetComponent<RoomController>().setNorthWallActive(true);
		}
	}

	public void UnlockAllRooms() {
		for (int x = 0; x < nTiles; x++) {
			for (int y = 0; y < nTiles; y++) {
				rooms[x, y].GetComponent<RoomController>().UnlockAllDoors();
			}
		}
	}

	public void LockAllRooms() {
		for (int x = 0; x < nTiles; x++) {
			for (int y = 0; y < nTiles; y++) {
				rooms[x, y].GetComponent<RoomController>().LockAllDoors();
			}
		}
	}
}
