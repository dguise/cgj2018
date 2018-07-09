using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public GameObject player1;      
    public GameObject player2;
    private Vector3 offset;        

    void Start () 
    {
        offset = transform.position - (player1.transform.position + player2.transform.position)/2;
    }
    
    void LateUpdate () 
    {
        transform.position = (player1.transform.position + player2.transform.position)/2 + offset;
    }
}
