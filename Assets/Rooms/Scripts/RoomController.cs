using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public static class Direction
    {
        public const string NORTH = "North/";
        public const string EAST = "East/";
        public const string SOUTH = "South/";
        public const string WEST = "West/";
    }

    private Dictionary<string, Transform> doors = new Dictionary<string, Transform>();
    private Light light;
    public float lightTriggerRadius = 15f;
    public float lightMaxRadius = 2f;
    public float maxLight = 30;
    private float magicFociNumber = 3.47f;

    void Awake()
    {
        doors.Add(Direction.NORTH + "Door", transform.Find(Direction.NORTH + "Door"));
        doors.Add(Direction.EAST + "Door", transform.Find(Direction.EAST + "Door"));
        doors.Add(Direction.SOUTH + "Door", transform.Find(Direction.SOUTH + "Door"));
        doors.Add(Direction.WEST + "Door", transform.Find(Direction.WEST + "Door"));

        SetAllDoors(true);

        light = transform.Find("Light").gameObject.GetComponent<Light>();
    }

    void Update()
    {
        var foci = Mathf.Sqrt(Mathf.Pow(lightTriggerRadius + magicFociNumber, 2) + Mathf.Pow(lightTriggerRadius, 2));  // major axis^2 - minor axis^2 

        Vector2 leftFociPoint = new Vector2(light.transform.position.x - foci, light.transform.position.y);
        Vector2 rightFociPoint = new Vector2(light.transform.position.x + foci, light.transform.position.y);
        var totalDistanceFromFoci = Vector2.Distance(leftFociPoint, PlayerManager.PlayerObjects[0].transform.position) + Vector2.Distance(rightFociPoint, PlayerManager.PlayerObjects[0].transform.position);
        //if (totalDistanceFromFoci < 50)
        //Debug.Log("FociDistance = " + totalDistanceFromFoci + " lighttriggerradius = " + lightTriggerRadius);

        if (totalDistanceFromFoci < 51)
        {
            light.gameObject.SetActive(true);
            float normdist = Mathf.Max(Vector2.Distance(PlayerManager.PlayerObjects[0].transform.position, light.transform.position) - lightMaxRadius, 0);
            float factor = 1 - normdist / (lightTriggerRadius - lightMaxRadius);
            light.range = maxLight * factor;


            //if (Vector2.Distance(PlayerManager.PlayerObjects[0].transform.position, light.transform.position) < lightTriggerRadius)
            //{
            //light.gameObject.SetActive(true);
            //float normdist = Mathf.Max(Vector2.Distance(PlayerManager.PlayerObjects[0].transform.position, light.transform.position) - lightMaxRadius, 0);
            //float factor = 1 - normdist / (lightTriggerRadius - lightMaxRadius);
            //light.range = maxLight * factor;
        }
        else
        {
            light.gameObject.SetActive(false);
        }

        // TODO: Fix for both players
    }

    //Keep if needed to update foci calculations
    //private void OnDrawGizmos()
    //{
    //    var foci = Mathf.Sqrt(Mathf.Pow(lightTriggerRadius + magicFociNumber, 2) + Mathf.Pow(lightTriggerRadius, 2));  // major axis^2 - minor axis^2 
        
    //    Gizmos.DrawWireSphere(new Vector3(light.transform.position.x - foci, light.transform.position.y, 0), 5.0f);
    //    Gizmos.DrawWireSphere(new Vector3(light.transform.position.x + foci, light.transform.position.y, 0), 5.0f);
    //}

    public void SetDoor(bool open, string direction)
    {
        Transform door = doors[direction + "Door"];
        Transform fence = transform.Find(direction + "fence");
        door.gameObject.SetActive(!open);
        fence.gameObject.SetActive(!open);
    }

    public void SetAllDoors(bool open)
    {
        SetDoor(open, Direction.NORTH);
        SetDoor(open, Direction.EAST);
        SetDoor(open, Direction.SOUTH);
        SetDoor(open, Direction.WEST);
    }
}