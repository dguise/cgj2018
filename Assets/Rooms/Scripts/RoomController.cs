using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomController : MonoBehaviour {
	public static class Direction {
		public const string NORTH = "North";
		public const string EAST = "East";
		public const string SOUTH = "South";
		public const string WEST = "West";
	}

	private enum State {
		Active,
		Inactive,
		Finished
	}
	private State state = State.Inactive;

	private Dictionary<string, DoorController> doors = new Dictionary<string, DoorController>();
	private Light light;
	public float lightTriggerRadius = 15f;
	public float lightMaxRadius = 2f;
	public float maxLight = 30;
    // private float magicFociNumber = 3.47f;
	
	public GameObject[] monsters;
	public Vector2[] spawnPoints;

	private List<GameObject> myMonsters = new List<GameObject>(); 

	private bool isActive = false;
	private GameObject fog;

	private const bool SPAWNMONSTERS = true;

	void Awake () {
		doors.Add(Direction.EAST, transform.Find(Direction.EAST).GetComponent<DoorController>());
		doors.Add(Direction.SOUTH, transform.Find(Direction.SOUTH).GetComponent<DoorController>());

		light = transform.Find("Light").gameObject.GetComponent<Light>();

		fog = transform.Find("Fog").gameObject;
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
		// TODO: Fix for both players ^

		if (SPAWNMONSTERS && !fog.activeInHierarchy && state == State.Inactive) {
			SpawnMonsters();
			state = State.Active;
		}

		// Does room still have MORTAL ENEMYYYY?
		if (state == State.Active && myMonsters.Where(x => x != null).Count() == 0) {
			isActive = false;
			state = State.Finished;
			GetComponentInParent<RoomSpawner>().UnlockAllRooms();
		}
	}

	// PERS ELLIPSE FOH-SIGH SHIZ

    // void Update()
    // {
    //     var foci = Mathf.Sqrt(Mathf.Pow(lightTriggerRadius + magicFociNumber, 2) + Mathf.Pow(lightTriggerRadius, 2));  // major axis^2 - minor axis^2 

    //     Vector2 leftFociPoint = new Vector2(light.transform.position.x - foci, light.transform.position.y);
    //     Vector2 rightFociPoint = new Vector2(light.transform.position.x + foci, light.transform.position.y);
    //     var totalDistanceFromFoci = Vector2.Distance(leftFociPoint, PlayerManager.PlayerObjects[0].transform.position) + Vector2.Distance(rightFociPoint, PlayerManager.PlayerObjects[0].transform.position);
    //     //if (totalDistanceFromFoci < 50)
    //     //Debug.Log("FociDistance = " + totalDistanceFromFoci + " lighttriggerradius = " + lightTriggerRadius);

    //     if (totalDistanceFromFoci < 51)
    //     {
    //         light.gameObject.SetActive(true);
    //         float normdist = Mathf.Max(Vector2.Distance(PlayerManager.PlayerObjects[0].transform.position, light.transform.position) - lightMaxRadius, 0);
    //         float factor = 1 - normdist / (lightTriggerRadius - lightMaxRadius);
    //         light.range = maxLight * factor;


    //         //if (Vector2.Distance(PlayerManager.PlayerObjects[0].transform.position, light.transform.position) < lightTriggerRadius)
    //         //{
    //         //light.gameObject.SetActive(true);
    //         //float normdist = Mathf.Max(Vector2.Distance(PlayerManager.PlayerObjects[0].transform.position, light.transform.position) - lightMaxRadius, 0);
    //         //float factor = 1 - normdist / (lightTriggerRadius - lightMaxRadius);
    //         //light.range = maxLight * factor;
    //     }
    //     else
    //     {
    //         light.gameObject.SetActive(false);
    //     }

    //     // TODO: Fix for both players
    // }

    //Keep if needed to update foci calculations
    //private void OnDrawGizmos()
    //{
    //    var foci = Mathf.Sqrt(Mathf.Pow(lightTriggerRadius + magicFociNumber, 2) + Mathf.Pow(lightTriggerRadius, 2));  // major axis^2 - minor axis^2 
        
    //    Gizmos.DrawWireSphere(new Vector3(light.transform.position.x - foci, light.transform.position.y, 0), 5.0f);
    //    Gizmos.DrawWireSphere(new Vector3(light.transform.position.x + foci, light.transform.position.y, 0), 5.0f);
    //}

	private void SpawnMonsters() {
		// Create a new spawnpoint from the first
		// Vector2 myFirstSpawnPoint = spawnPoints[0];
		// myFirstSpawnPoint.x -= 2;

		// GameObject initMon = monsters[Random.Range(0, monsters.Length)];
		// GameObject myMon = Instantiate(initMon, myFirstSpawnPoint, Quaternion.identity);
		// myMonsters.Add(myMon);

		foreach (Vector2 spawnPoint in spawnPoints) {
			if (Random.Range(0f, 1f) < 0.8) {
				Vector2 temp = spawnPoint;
				temp.x += transform.position.x;
				temp.y += transform.position.y;
				GameObject mon = monsters[Random.Range(0, monsters.Length)];
				GameObject obj = Instantiate(mon, temp, mon.transform.rotation);
				myMonsters.Add(obj);
			}
		}
	}

	public void SetDoor (bool open, string direction) {
		DoorController door = doors[direction];
		door.LockDoor();
	}

	public void SetAllDoors(bool open) {
		SetDoor(open, Direction.EAST);
		SetDoor(open, Direction.SOUTH);
	}

	public void LockAllDoors() {
		doors[Direction.EAST].LockDoor();
		doors[Direction.SOUTH].LockDoor();
	}

	public void UnlockAllDoors() {
		doors[Direction.EAST].UnlockDoor();
		doors[Direction.SOUTH].UnlockDoor();
	}
}