using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof (Rigidbody2D))]

public class Player : Unit {
	public float playerSpeed = 5f;
	public IWeapon weapon;
	float radius = 0.5f;
	const float DEADZONE = 0.6f;
	public int playerID;

    Transform head;

	// Use this for initialization
	void Start () {
        weapon = new Gun();
        head = transform.Find("monkeyhead");
	}
	
	// Update is called once per frame
	void Update () {

		
	}

	private void FixedUpdate() {
		Vector2 playerVelocity = new Vector2(Input.GetAxisRaw(Inputs.Horizontal(playerID)), Input.GetAxisRaw(Inputs.Vertical(playerID)));
        GetComponent<Rigidbody2D>().velocity = playerVelocity * playerSpeed;
		
		Vector2 attackDirection = new Vector2(Input.GetAxisRaw(Inputs.FireHorizontal(playerID)), Input.GetAxisRaw(Inputs.FireVertical(playerID)));

		Vector2 attackPosition = transform.position;
		Quaternion attackRotation = transform.rotation;

		if (attackDirection.magnitude > DEADZONE) {
			weapon.Attack(attackPosition, attackDirection.normalized, attackRotation, radius);
            head.eulerAngles = new Vector3(head.eulerAngles.x, head.eulerAngles.y, (Mathf.Atan2(attackDirection.y, attackDirection.x) * 180 / Mathf.PI) * -1 - 90);
        }
    }
}
