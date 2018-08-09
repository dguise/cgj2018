using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    bool allDead = false;

    void Start()
    {
        cam = GetComponent<Camera>();
        offset = new Vector3(0, 0, -10);
        players = PlayerManager.PlayerObjects;
        foreach (var player in players)
            player.GetComponent<Unit>().OnDeath += HandlePlayerDeath;
    }

    private void HandlePlayerDeath(Unit unit)
    {
        if (PlayerManager.PlayersAlive.Count == 0)
            allDead = true;
    }

    private void LateUpdate()
    {
        if (!allDead)
        {
            Move();
            ZoomPerspective();
        }
    }

    private int FindPlayers()
    {
        players.Clear();
        players.AddRange(GameObject.FindGameObjectsWithTag(Tags.Player));
        return players.Count;
    }

    private void Move()
    {
        wantedPosition = Vector2.zero;
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] != null)
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
        for (int i = 0; i < players.Count; i++)
        {
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
        if (players[0] == null) return;
        float distance = Vector3.Scale(wantedPosition - players[0].transform.position, new Vector3(1, 1, 0)).magnitude;
        float oppositeCathethus = Mathf.Abs(distance);
        float newDistance = oppositeCathethus / (Mathf.Tan(Mathf.PI / 180 * cam.fieldOfView / 2));
        //Debug.Log("OppositeCathetus: " + oppositeCathethus);
        //Debug.Log("New Distance: " + newDistance);
        newDistance = Mathf.Max(newDistance / 2 + extraSize, 13f);
        cam.transform.position = new Vector3(cam.transform.position.x,
                                                cam.transform.position.y,
                                                -newDistance);

    }

}