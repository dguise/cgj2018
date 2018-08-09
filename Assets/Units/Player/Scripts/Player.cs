using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : Unit
{
    public Weapon weapon;
    public Ability ability;
    public PlayerManager.CharacterClasses PlayerClass;
    public Animator anim { get; set; }

    public PlayerIndex playerIndex { get; set; }
    GamePadState state;
    GamePadState prevState;
    const float DEADZONE = 0.70f;

    float radius = 0f;
    float originalMovementSpeed;

    Transform head;
    Transform body;
    private Transform bodyMesh;

    void Start()
    {
        weapon = PlayerManager.GetWeapon(PlayerClass, gameObject);
        ability = PlayerManager.GetAbility(PlayerClass, gameObject);

        head = transform.Find("monkeyhead");
        body = transform.Find("Armature.001");
        bodyMesh = transform.Find("Cube.001");

        originalMovementSpeed = movementSpeed;

        anim = GetComponent<Animator>();

        DontDestroyOnLoad(this);

        Stats.OnLevelUp += HandleLevelUp;
    }

    void HandleLevelUp()
    {
        Debug.Log("You are now level " + Stats.Level);
        TextManager.CreateHealText((Stats.Level).ToString(), transform, 0.2f);

        ParticleSpawner.instance.SpawnParticleEffect(transform.position, ParticleTypes.LevelUp, parent: transform);
        // Do Leveling stuff
        // Particle?
        // Add ability? 
    }

    private void Update()
    {
        state = GamePad.GetState(playerIndex);

        if (!Stats.CanMove || IsDead) return;

        if (state.Buttons.A.IsDown() && prevState.Buttons.A.IsUp())
        {
            if (ability.CanUse)
                ability.Use();
        }

        ApplyStatusEffects();

        prevState = state;
    }

    private void ApplyStatusEffects()
    {
        movementSpeed = (originalMovementSpeed * (1 + (Stats.Agility / 100f)));

        if (Stats.HasStatus(Statuses.Slowed))
            movementSpeed = movementSpeed * 0.5f;
        if (Stats.HasStatus(Statuses.Bleeding))
            TakeDamage(10f * Time.deltaTime, gameObject, GetComponent<Collider2D>());
        if (bodyMesh.gameObject.activeInHierarchy == !Stats.HasStatus(Statuses.Invisible))
            bodyMesh.gameObject.SetActive(!Stats.HasStatus(Statuses.Invisible));
    }

    private void FixedUpdate()
    {
        if (!Stats.CanMove || IsDead) return;

        var horizontalLeft = state.ThumbSticks.Left.X;
        var verticalLeft = state.ThumbSticks.Left.Y;
        Vector2 playerInput = new Vector2(horizontalLeft, verticalLeft);
        RigidBody.velocity = playerInput.normalized * movementSpeed;

        if (RigidBody.velocity.magnitude > DEADZONE)
        {
            UnitBodyDirection = RigidBody.velocity.normalized; 
            var direction = (Mathf.Atan2(RigidBody.velocity.x, RigidBody.velocity.y) * Mathf.Rad2Deg) + 180;
            body.eulerAngles = new Vector3(body.eulerAngles.x, body.eulerAngles.y, direction);
        }

        // Attack
        var horizontalRight = state.ThumbSticks.Right.X;
        var verticalRight = state.ThumbSticks.Right.Y;
        Vector2 attackDirection = new Vector2(horizontalRight, verticalRight);

        if (attackDirection.magnitude > DEADZONE)
        {
            head.eulerAngles = new Vector3(head.eulerAngles.x, head.eulerAngles.y, (Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg) * -1 - 90);
            var attackRotation = new Vector3(0, 0, (Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg) - 90);
            Vector2 attackPosition = transform.position;
            weapon.Attack(attackPosition, attackDirection, Quaternion.Euler(attackRotation), radius);
        }

        anim.SetFloat(AnimatorConstants.Speed, RigidBody.velocity.magnitude);
    }

    public override void TakeDamageExtender(float damage, GameObject sender, Collider2D collider)
    {
        GamePadHelper.Rumble(this, playerIndex, 0.5f, 1, 0);

        // If class is X heal yourself and all around with 15% of damage
        /*
         * If Player is killed, run a dialogue from its killer?
         * var EvilDoer = sender != null ? (sender.GetComponent<Unit>()) : null;
         * var EvilDoerPortrait = (EvilDoer != null ? EvilDoer.Portrait : null);*/
    }

    public void Revive(Player sender)
    {
        this.Health = this.maxHealth / 2;
        SoundManager.instance.PlayAudio(14);
        this.transform.rotation = sender.transform.rotation; // Copy the other players rotation 
        RigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;

        ParticleSpawner.instance.SpawnParticleEffect(transform.position, ParticleTypes.BlueGlitter_OverTime, lifetime: 2);

        // Penalty for reviving
        sender.Stats.SetStatus(this, 3, Statuses.Slowed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Revive player on touch
        var player = collision.collider.GetComponent<Player>();

        if (player != null && player.IsDead)
        {
            player.Revive(this);
        }
    }
}
