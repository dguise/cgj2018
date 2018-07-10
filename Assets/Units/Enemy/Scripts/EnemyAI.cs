using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : Unit {

    Rigidbody2D rb;
    Transform start;
    Transform target;

    float movementspeed = 1.5f;
    float breakRange = 6;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        start = Instantiate<GameObject>(new GameObject(), transform.position, Quaternion.identity).transform;
	}
	
	void FixedUpdate () {
		if (target != null)
        {
            rb.velocity = (target.position - transform.position).normalized * movementspeed;
        }
        if (target == start && Vector2.Distance(transform.position, target.position) < 0.5)
        {
            target = null;
            rb.velocity = Vector2.zero;
        } 
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == Tags.Player && target == null)
        {
            Target(collider.transform);
        }
    }

    private Coroutine _aggroCooldown;
    private void Target(Transform targetTransform)
    {
        target = targetTransform;
        if (_aggroCooldown != null)
            StopCoroutine(_aggroCooldown);
        _aggroCooldown = StartCoroutine(ResetAggro());
    }

    IEnumerator ResetAggro()
    {
        yield return new WaitForSeconds(5);
        var distance = Vector2.Distance(target.position, start.position);

        if (distance > 15)
        {
            target = start;
        } else
        {
            StartCoroutine(ResetAggro());
        }
    }

    public override void TakeDamageExtender(float damage, GameObject sender, Collision2D collision)
    {
        Target(sender.transform);
    }
}
