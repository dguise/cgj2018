using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof (Rigidbody2D))]

public class PlayerMovement : MonoBehaviour {
	public float playerSpeed = 1f;
	public IWeapon weapon;
	float radius = 1f;

	// Use this for initialization
	void Start () {
		weapon = new Bullet();
	}
	
	// Update is called once per frame
	void Update () {

		
	}

	private void FixedUpdate() {
		Vector2 playerVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        GetComponent<Rigidbody2D>().velocity = playerVelocity * playerSpeed;
		
		Vector2 attackDirection = new Vector2(Input.GetAxisRaw("FireHorizontal"), Input.GetAxisRaw("FireVertical"));

		Vector2 attackPosition = transform.position;
		Quaternion attackRotation = transform.rotation;

		
		if (attackDirection != Vector2.zero) {
			weapon.Attack(attackPosition, attackDirection, attackRotation, radius);
		}

		Debug.Log(attackDirection);
	}
}
