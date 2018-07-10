using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof (Rigidbody2D))]
public class Player : Unit {
	private float playerSpeed = 3f;
	public Weapon weapon;
	float radius = 0.5f;
	const float DEADZONE = 0.6f;
	public int playerID;
	Animator anim;

    Transform head;
    Transform body;

    Rigidbody2D rb;

	void Start () {
        weapon = new Gun(gameObject);

        head = transform.Find("monkeyhead");
        body = transform.Find("Armature.001");

		anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        Health = 1500;
	}


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            weapon = new Gun(gameObject);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            weapon = new SpecialGun(gameObject);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            weapon = new ShieldGun(gameObject);
    }

    private void FixedUpdate() {
		Vector2 playerVelocity = new Vector2(Input.GetAxisRaw(Inputs.Horizontal(playerID)), Input.GetAxisRaw(Inputs.Vertical(playerID)));
        rb.velocity = playerVelocity * playerSpeed;
        anim.SetFloat(AnimatorConstants.Speed, playerVelocity.magnitude);

        if (rb.velocity.magnitude > DEADZONE)
        {
            body.eulerAngles = new Vector3(body.eulerAngles.x, body.eulerAngles.y, (Mathf.Atan2(rb.velocity.x, rb.velocity.y) * 180 / Mathf.PI) + 180);
        }

        Vector2 attackDirection = new Vector2(Input.GetAxisRaw(Inputs.FireHorizontal(playerID)), Input.GetAxisRaw(Inputs.FireVertical(playerID)));

		Vector2 attackPosition = transform.position;
		Quaternion attackRotation = Quaternion.identity;

		if (attackDirection.magnitude > DEADZONE) {
			weapon.Attack(attackPosition, attackDirection, attackRotation, radius);
            head.eulerAngles = new Vector3(head.eulerAngles.x, head.eulerAngles.y, (Mathf.Atan2(attackDirection.y, attackDirection.x) * 180 / Mathf.PI) * -1 - 90);
        }
    }

    public override void TakeDamageExtender(float damage, GameObject sender, Collider2D collider)
    {
        // Currently don't give af
    }
}
    