using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowZoomScript : MonoBehaviour 
{
#pragma warning disable 0108
    private Camera camera;
#pragma warning restore 0108
	Vector3 offset;

	public List<GameObject> players = new List<GameObject>();
	float zoomSpeed = 10f;
	float dampTime = 0.1f;
	public float extraSize = 7f;
	float minSize = 7f;
	Vector3 wantedPosition;
	public int foundPlayers = 0;
	

	// Use this for initialization
	void Start () 
	{
        offset = new Vector3(0, 0, -10);
	}
	
	private void FixedUpdate() 
	{
		if (foundPlayers < PlayerManager.players) {
			foundPlayers = FindPlayers();
		}

		if (foundPlayers > PlayerManager.players) {
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
        camera = GetComponent<Camera>();

    }

	private int FindPlayers() {
		int found = 0;
		for (int i = 0; i < PlayerManager.players; i++) {
			GameObject player = PlayerManager.PlayerObjects[i];
			if (player && !players.Contains(player)) {
				players.Add(player);
				found += 1;
			}
		}

		return found;
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

        camera.orthographicSize = Mathf.SmoothDamp(camera.orthographicSize, size, ref zoomSpeed, dampTime);
	}

	private void ZoomPerspective()
	{

		float distance = Vector3.Scale(wantedPosition - players[0].transform.position, new Vector3(1, 1, 0)).magnitude;
		float oppositeCathethus = Mathf.Abs(distance);
		float newDistance = oppositeCathethus/(Mathf.Tan(Mathf.PI/180 *camera.fieldOfView/2));
		//Debug.Log("OppositeCathetus: " + oppositeCathethus);
		//Debug.Log("New Distance: " + newDistance);
		newDistance = Mathf.Max(newDistance/2 + extraSize, 13f);
        camera.transform.position = new Vector3(camera.transform.position.x, 
												camera.transform.position.y,
												-newDistance);

	}

}