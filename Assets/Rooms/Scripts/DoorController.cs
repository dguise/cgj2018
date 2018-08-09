using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private GameObject door;
    private GameObject fence;
    private GameObject doorLight;
    private CircleCollider2D trigger;

    public enum State
    {
        Locked,
        Unlocked,
        Finished
    }
    public State state = State.Locked;

    private float timestamp;
    private float unlockTime = 1f;

    public float lightRadius = 10f;

    void Awake()
    {
        door = transform.Find("Door").gameObject;
        fence = transform.Find("fence").gameObject;
        doorLight = transform.Find("DoorLight").gameObject;
        trigger = GetComponent<CircleCollider2D>();
        CloseDoor();
        LockDoor();
    }

    void Update()
    {
        if (PlayerManager.PlayerObjects.Count == 0)
        {
            return;
        }
        if (state == State.Unlocked)
        {
            doorLight.SetActive(
                Vector2.Distance(PlayerManager.PlayerObjects[0].transform.position, doorLight.transform.position) < lightRadius ||
                (PlayerManager.PlayerObjects.Count == 2 && Vector2.Distance(PlayerManager.PlayerObjects[1].transform.position, doorLight.transform.position) < lightRadius));
        }
    }

    public void OpenDoor()
    {
        door.SetActive(false);
        fence.SetActive(false);
        trigger.enabled = false;
        doorLight.SetActive(false);
        state = State.Finished;
    }

    public void CloseDoor()
    {
        door.SetActive(true);
        fence.SetActive(true);
    }

    public void LockDoor()
    {
        if (state != State.Finished)
        {
            doorLight.SetActive(false);
            trigger.enabled = false;
            state = State.Locked;
        }
    }

    public void UnlockDoor()
    {
        if (state != State.Finished)
        {
            doorLight.SetActive(true);
            trigger.enabled = true;
            timestamp = Time.time;
            state = State.Unlocked;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == Tags.Player && Time.time - timestamp > unlockTime)
        {
            GetComponentInParent<RoomSpawner>().LockAllRooms();
            OpenDoor();
        }
    }
}
