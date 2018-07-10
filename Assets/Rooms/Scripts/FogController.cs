using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogController : MonoBehaviour {
	public GameObject fog;

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Player") {
			Destroy(fog);
			Destroy(gameObject);
		}
	}
}
