using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit {

    Rigidbody2D rb;
    Transform start;
    Transform target;
    bool targetIsPlayer = false;

    Weapon weapon;
    [Range(1, 20)]
    public float AttackRange = 4;

    float movementspeed = 1.5f;

    private bool readyToChangeAggro = true;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        start = Instantiate<GameObject>(new GameObject(), transform.position, Quaternion.identity).transform;
        weapon = new Gun(gameObject);
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

    private void Update()
    {
        if (targetIsPlayer && Vector2.Distance(target.position, transform.position) <= AttackRange)
        {
            weapon.Attack(transform, target.transform);
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
    private Coroutine _aggroGiveUpTimer;
    private void Target(Transform targetTransform)
    {
        target = targetTransform;
        targetIsPlayer = target.tag == Tags.Player;

        if (_aggroGiveUpTimer != null)
            StopCoroutine(_aggroCooldown);
        _aggroGiveUpTimer = StartCoroutine(GiveUpAggro());


        if (_aggroCooldown != null)
            StopCoroutine(_aggroCooldown);
        _aggroCooldown = StartCoroutine(CooldownAggro());
        
    }

    IEnumerator GiveUpAggro()
    {
        yield return new WaitForSeconds(5);
        if (target == null)
            StopCoroutine(_aggroGiveUpTimer);

        var distanceToHome = Vector2.Distance(target.position, start.position);
        var distanceToTarget = Vector2.Distance(target.position, transform.position);

        if (distanceToHome > 15 && distanceToTarget > 4)
        {
            Target(start);
        } else
        {
            StartCoroutine(GiveUpAggro());
        }
    }

    IEnumerator CooldownAggro()
    {
        readyToChangeAggro = false;
        yield return new WaitForSeconds(3);
        readyToChangeAggro = true;
    }

    public override void TakeDamageExtender(float damage, GameObject sender, Collision2D collision)
    {
        if (readyToChangeAggro && sender.tag == Tags.Player)
            Target(sender.transform);
    }
}
