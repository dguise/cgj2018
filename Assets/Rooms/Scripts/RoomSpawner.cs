using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour {
	public int nTiles = 7;
	public int tileSize = 1;
	private List<GameObject> list = new List<GameObject>();

	// Use this for initialization
	void Start () {
		Object[] subListObjects = Resources.LoadAll("Prefabs", typeof(GameObject));
		foreach (GameObject subListObject in subListObjects) {    
			GameObject lo = (GameObject)subListObject;
			list.Add(lo);
		}

		for(int x = -(nTiles - 1) / 2; x <= (nTiles - 1) / 2; x++) {
			for(int y = -(nTiles - 1) / 2; y <= (nTiles - 1) / 2; y++) {
				Vector3 v = new Vector3(x * tileSize, y * tileSize, 0);
				int r = Random.Range(0, list.Count);
				Object.Instantiate(list[r], v, Quaternion.identity, transform);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
