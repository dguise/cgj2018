
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowZoomScript : MonoBehaviour 
{

	private Camera camera;
	GameObject player1;
	GameObject player2;
	Vector3 offset;
	float zoomSpeed = 10f;
	float dampTime = 0.1f;
	float extraSize = 2f;
	float minSize = 7f;
	float maxSize = 40f;
	Vector3 wantedPosition;
	

	// Use this for initialization
	void Start () 
	{
        offset = new Vector3(0, 0, -10);
	}
	
	private void FixedUpdate() 
	{
		Move();
		Zoom();
	}

	private void Awake ()
    {
		player1 = GameObject.Find("Player1");
		player2 = GameObject.Find("Player2");

		//If no player exist
		if (!player1 && !player2) {
			Debug.Log("Players names Player1 and Player2 needs to exist in the scene");
			Destroy(this);
		}

		// If only one of them exist, execute as normally but with both players as same player.
		if (!player1) {
			player2 = player1;
		} else if (!player2) {
			player1 = player2;
		}

        camera = GetComponent<Camera>();
    }

	private void Move() 
	{

        wantedPosition = (player1.transform.position + player2.transform.position)/2 + offset;
		transform.position = wantedPosition;
	}

	private void Zoom() 
	{
		Vector3 wantedLocalPosition = transform.InverseTransformPoint(wantedPosition);
		float size = 0f;

		// Otherwise, find the position of the target in the camera's local space.
		Vector3 targetLocalPos1 = transform.InverseTransformPoint(player1.transform.position);
		Vector3 targetLocalPos2 = transform.InverseTransformPoint(player2.transform.position);
		Vector3 desiredPosToTarget1 = targetLocalPos1 - wantedLocalPosition;
		Vector3 desiredPosToTarget2 = targetLocalPos2 - wantedLocalPosition;

		float sizeY = (Mathf.Abs(desiredPosToTarget1.y) + Mathf.Abs(desiredPosToTarget2.y))/2;
		float sizeX = (Mathf.Abs(desiredPosToTarget1.x) + Mathf.Abs(desiredPosToTarget2.x))/2;
		size = Mathf.Max(sizeY, sizeX);

        size += extraSize;
        size = Mathf.Max(size, minSize);

        camera.orthographicSize = Mathf.SmoothDamp(camera.orthographicSize, size, ref zoomSpeed, dampTime);
	}

}