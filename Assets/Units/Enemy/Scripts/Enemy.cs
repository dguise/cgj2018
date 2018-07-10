﻿using System;
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
    [Range(0, 10)]
    public float AggroRange = 5;

    Vector3 randomOffset;


    private bool readyToChangeAggro = true;

    GameObject[] players;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        start = Instantiate<GameObject>(new GameObject(), transform.position, Quaternion.identity).transform;
        weapon = new Gun(gameObject);
        players = GameObject.FindGameObjectsWithTag(Tags.Player);

        Health = 30;
        randomOffset = new Vector2(UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-5f, 5f));
        movementspeed = movementspeed * UnityEngine.Random.Range(0.8f, 1.3f);
	}
	
	void FixedUpdate () {
        // If you're close to the target, remove the target
        if (target != null && Vector2.Distance(transform.position, target.position) < 0.5 && !targetIsPlayer)
        {
            target = null;
            rb.velocity = Vector2.zero;
        }
        if (target != null)
        {
            rb.velocity = ((target.position + randomOffset) - transform.position).normalized * movementspeed;
        }
        if (targetIsPlayer && Vector2.Distance(target.position, transform.position) < AttackRange-1)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void Update()
    {
        if (targetIsPlayer && Vector2.Distance(target.position, transform.position) <= AttackRange)
        {
            weapon.Attack(transform, target.transform);
        }

        foreach (var player in players)
        {
            var distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer < AggroRange && target == null)
                Target(player.transform);
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
        if (target != null)
        {
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
    }

    IEnumerator CooldownAggro()
    {
        readyToChangeAggro = false;
        yield return new WaitForSeconds(3);
        readyToChangeAggro = true;
    }

    public override void TakeDamageExtender(float damage, GameObject sender, Collider2D collider)
    {
        // Maybe run away if taking damage? Random?
        if (readyToChangeAggro && sender.tag == Tags.Player)
            Target(sender.transform);
    }
}
