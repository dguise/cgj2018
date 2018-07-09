using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof (Rigidbody2D))]

public class Player : MonoBehaviour {
	public float playerSpeed = 5f;
	public IWeapon weapon;
	float radius = 0.5f;
	const float DEADZONE = 0.6f;

	// Use this for initialization
	void Start () {
        weapon = new Gun();
	}
	
	// Update is called once per frame
	void Update () {

		
	}

	private void FixedUpdate() {
		Vector2 playerVelocity = new Vector2(Input.GetAxisRaw(Inputs.Horizontal), Input.GetAxisRaw(Inputs.Vertical));
		if (playerVelocity == Vector2.zero) {
			playerVelocity.x = Input.GetKey(KeyCode.D) ? 1 : Input.GetKey(KeyCode.A) ? -1 : 0;
			playerVelocity.y = Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 : 0;
		}
        GetComponent<Rigidbody2D>().velocity = playerVelocity * playerSpeed;
		
		Vector2 attackDirection = new Vector2(Input.GetAxisRaw(Inputs.FireHorizontal), Input.GetAxisRaw(Inputs.FireVertical));

		Vector2 attackPosition = transform.position;
		Quaternion attackRotation = transform.rotation;

		
		if (attackDirection.magnitude > DEADZONE) {
			weapon.Attack(attackPosition, attackDirection.normalized, attackRotation, radius);
		}
	}
}
