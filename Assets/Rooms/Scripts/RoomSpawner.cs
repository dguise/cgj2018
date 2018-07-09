using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour {
	public int nTiles = 7;
	public float tileSize = 1;
	public float wallThiccness = 0.1f;
	private List<GameObject> roomList = new List<GameObject>();
	private GameObject[,] rooms;

	// Use this for initialization
	void Start () {
		rooms = new GameObject[nTiles, nTiles];

		Object[] subListObjects = Resources.LoadAll("Prefabs", typeof(GameObject));
		foreach (GameObject subListObject in subListObjects) {    
			GameObject lo = (GameObject)subListObject;
			roomList.Add(lo);
		}

		for(int x = -(nTiles - 1) / 2; x <= (nTiles - 1) / 2; x++) {
			for(int y = -(nTiles - 1) / 2; y <= (nTiles - 1) / 2; y++) {
				Vector3 v = new Vector3(x * (tileSize - wallThiccness), y * (tileSize - wallThiccness), 0);
				int r = Random.Range(0, roomList.Count);
				int xroom = x + (nTiles - 1) / 2;
				int yroom = y + (nTiles - 1) / 2;
				rooms[xroom, yroom] = Object.Instantiate(roomList[r], v, Quaternion.identity, transform);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
