using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallWin : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject manager = GameObject.Find("ManagerManager");
		CustomGameManager gm = manager.GetComponent<CustomGameManager>();
		gm.Victory();
	}
}
