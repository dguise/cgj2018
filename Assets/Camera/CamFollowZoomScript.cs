using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowZoomScript : MonoBehaviour 
{
    private Camera cam;
	Vector3 offset;

	public List<GameObject> players = new List<GameObject>();
	float zoomSpeed = 10f;
	float dampTime = 0.1f;
	public float extraSize = 7f;
	float minSize = 7f;
	Vector3 wantedPosition;
	public int foundPlayers = 0;
	public int playersReady;
	

	// Use this for initialization
	void Start () 
	{
        offset = new Vector3(0, 0, -10);
	}
	
	private void FixedUpdate() 
	{
		playersReady = PlayerManager.playersReady;
		if (foundPlayers < PlayerManager.playersReady) {
			foundPlayers = FindPlayers();
		}

		if (foundPlayers > PlayerManager.playersReady) {
			int amountOfPlayers = 0;
			int alivePlayer = -1;
			for (int i = 0; i < PlayerManager.playerReady.Length; i++) {
				if(PlayerManager.playerReady[i]) {
					amountOfPlayers += 1;
					alivePlayer = i;
				} 
			}

			if (amountOfPlayers == 0) {
				return;
			}

			Debug.Log("amountOfPlayers: " + amountOfPlayers);
			foundPlayers = amountOfPlayers;

			for (int i = 0; i < players.Count; i++) {
				if (!PlayerManager.playerReady[i]) {
					players[i] = players[alivePlayer];
				}
			}
		} 
		
		if (foundPlayers > 0) {
			Move();
			ZoomPerspective();
		}
	}

	private void Awake ()
    {
        cam = GetComponent<Camera>();
    }

	private int FindPlayers() {
        players.Clear();
        players.AddRange(GameObject.FindGameObjectsWithTag(Tags.Player));
        return players.Count;
		//for (int i = 0; i < PlayerManager.players; i++) {
		//	GameObject player = PlayerManager.PlayerObjects[i];
		//	if (player != null && !players.Contains(player)) {
		//		//players.Add(player);
		//		//found += 1;
		//	}
		//}

		//return found;
	}

	private void Move() 
	{
		wantedPosition = Vector2.zero;
		for (int i = 0; i < players.Count; i++) {
        	wantedPosition += players[i].transform.position;
		}

		wantedPosition /= players.Count;
		wantedPosition += offset;
		transform.position = wantedPosition;
	}

	private void ZoomOrthogonal() 
	{
		Vector3 wantedLocalPosition = transform.InverseTransformPoint(wantedPosition);
		float size = 0f;
		float sizeY = 0f;
		float sizeX = 0f;

		// Otherwise, find the position of the target in the camera's local space.
		for (int i = 0; i < players.Count; i++) {
			Vector3 targetLocalPos = transform.InverseTransformPoint(players[i].transform.position);
			Vector3 desiredPosToTarget = targetLocalPos - wantedLocalPosition;
			sizeY += Mathf.Abs(desiredPosToTarget.y);
			sizeX += Mathf.Abs(desiredPosToTarget.x);
		}

		sizeY /= players.Count;
		sizeX /= players.Count;

		size = Mathf.Max(sizeY, sizeX);

        size += extraSize;
        size = Mathf.Max(size, minSize);

        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, size, ref zoomSpeed, dampTime);
	}

	private void ZoomPerspective()
	{

		float distance = Vector3.Scale(wantedPosition - players[0].transform.position, new Vector3(1, 1, 0)).magnitude;
		float oppositeCathethus = Mathf.Abs(distance);
		float newDistance = oppositeCathethus/(Mathf.Tan(Mathf.PI/180 *cam.fieldOfView/2));
		//Debug.Log("OppositeCathetus: " + oppositeCathethus);
		//Debug.Log("New Distance: " + newDistance);
		newDistance = Mathf.Max(newDistance/2 + extraSize, 13f);
        cam.transform.position = new Vector3(cam.transform.position.x, 
												cam.transform.position.y,
												-newDistance);

	}

}