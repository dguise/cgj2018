using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit {

    Animator anim;

    Transform start;
    [HideInInspector]
    public Transform target;
    bool targetIsPlayer = false;
    Unit targetUnit;

    Weapon weapon;
    [Range(0.2f, 20)]
    public float AttackRange = 4;
    public EnemyClass enemyClass;
    [Range(0, 10)]
    public float AggroRange = 5;

    public bool shouldDropPowerup = true;
    [Range(0f, 1f)]
    public float powerupDropRate = 0.1f;
    private bool willDropPowerup = false;

    public bool IsEliteEnemy = false;
    [Range(1, 3)]
    public float EliteSizeIncrease = 1.2f;

    public Boolean RandomOffset = true;
    Vector3 randomOffset = Vector2.zero;

    private Vector3 targetPosition;
    private bool readyToChangeAggro = true;
    GameObject[] players;

    Transform body;

	void Start () {
        anim = GetComponent<Animator>();

        start = Instantiate<GameObject>(new GameObject(), transform.position, Quaternion.identity).transform;
        weapon = EnemyHelper.GetWeapon(enemyClass, gameObject);
        players = GameObject.FindGameObjectsWithTag(Tags.Player);

        if (RandomOffset)
        {
            InvokeRepeating("GenerateNewRandomOffset", 0, 3);
        }
        movementSpeed = movementSpeed * UnityEngine.Random.Range(0.7f, 1.15f);

        body = transform.FindWithTagInChildren("Body");

        willDropPowerup = powerupDropRate > UnityEngine.Random.Range(0f, 1f);

        if (IsEliteEnemy)
        {
            // Elites always drop powerups
            willDropPowerup = true;
            transform.localScale *= EliteSizeIncrease;
            AttackRange *= EliteSizeIncrease;

            shouldDropPowerup = true;
            ExperienceWorth *= 2;
            Stats.GainExperience(100000 * Stats.Level);
        }
	}
	void FixedUpdate ()
    {
        if (target != null)
        {
            targetPosition = target.position + randomOffset;

            if (Vector2.Distance(transform.position, target.position) < 3 && !targetIsPlayer)
            {
                target = null;
                RigidBody.velocity = Vector2.zero;
                return;
            }

            // Apply movement
            RigidBody.velocity = ((targetPosition) - transform.position).normalized * movementSpeed;
            // Apply rotation

            UnitBodyDirection = RigidBody.velocity.normalized;
            var direction = (Mathf.Atan2(RigidBody.velocity.x, RigidBody.velocity.y) * 180 / Mathf.PI) + 180;
            body.eulerAngles = new Vector3(body.eulerAngles.x, body.eulerAngles.y, direction);
            if (targetIsPlayer && Vector2.Distance(target.position, transform.position) < AttackRange-1)
            {
                RigidBody.velocity = Vector2.zero;
            }
            if (weapon.GetType() == typeof(MeleeGun) && targetIsPlayer && ((MeleeGun)weapon).isAttacking)
                RigidBody.velocity = Vector2.zero;

            if (anim != null)
                anim.SetFloat("Speed", RigidBody.velocity.magnitude);
        }

    }

    public void PerformAttack()
    {
        ((MeleeGun)weapon).PerformAttack();
    }

    private void Update()
    {
        // Do we need random offset here?
        if (target != null && targetIsPlayer && Vector2.Distance(target.position, transform.position) <= AttackRange)
        {
            targetUnit = target.GetComponent<Unit>();
            if (!targetUnit.Stats.Status.Contains(Statuses.Invisible) && !targetUnit.IsDead)
            {
                weapon.Attack(transform, target.transform);
            } else
            {
                Target(start);
            }
        }

        foreach (var player in players)
        {
            if (player == null || player.GetComponent<Unit>().Stats.Status.Contains(Statuses.Invisible)) continue;
            var distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer < AggroRange && target == null)
                Target(player.transform);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 255, 0.2f);
        Gizmos.DrawWireSphere(transform.position, AttackRange);
        Gizmos.color = new Color(255, 0, 0, 0.2f);
        Gizmos.DrawWireSphere(transform.position, AggroRange);

        if (target == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, target.position);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, targetPosition);
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
        if (readyToChangeAggro && sender != null && sender.tag == Tags.Player)
            Target(sender.transform);

        if (shouldDropPowerup && IsDead && willDropPowerup)
            PowerupManager.instance.SpawnRandomPowerUp(transform.position);

    }

    void GenerateNewRandomOffset()
    {
        randomOffset = new Vector2(UnityEngine.Random.Range(-3f, 3f), UnityEngine.Random.Range(-3f, 3f));
    }
}
