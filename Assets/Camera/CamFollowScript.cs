using UnityEngine;
using System.Collections;

public class CamFollowScript : MonoBehaviour {

    public GameObject player1;      
    public GameObject player2;
    private Vector3 offset;        

    void Start () 
    {
        offset = new Vector3(0, 0, -10);
    }
    
    void LateUpdate () 
    {
        transform.position = (player1.transform.position + player2.transform.position)/2 + offset;
    }
}
