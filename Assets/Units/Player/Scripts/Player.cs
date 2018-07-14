using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Player : Unit
{
    public Weapon weapon;
    public Ability ability;

    float radius = 0.5f;
    const float DEADZONE = 0.98f;
    public int playerID;
    public Animator anim { get; set; }
    float originalMovementSpeed;

    Transform head;
    Transform body;
    private Transform bodyMesh;
    [HideInInspector]
    public Rigidbody2D rb;

    public new Sprite UnitPortrait
    {
        get
        {
            //return null;
            return SendPredatorRetrieveActiveChild().GetComponent<Player_Head_Script>().Portrait;
        }
    }

    public PlayerManager.CharacterClassesEnum PlayerClass;

    void Start()
    {
        weapon = PlayerManager.GetWeapon(PlayerClass, gameObject);
        ability = PlayerManager.GetAbility(PlayerClass, gameObject);

        head = transform.Find("monkeyhead");
        body = transform.Find("Armature.001");
        bodyMesh = transform.Find("Cube.001");

        originalMovementSpeed = movementSpeed;

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        DontDestroyOnLoad(this);

        Stats.OnLevelUp = new Action(HandleLevelUp);
    }

    void HandleLevelUp()
    {
        Debug.Log("You are now level " + Stats.Level + " with exp: " + Stats.Experience);
        // Do Leveling stuff
        // Particle?
        // Add ability? 
    }

    public void UpdateMask(PlayerManager.CharacterClassesEnum aClass)
    {
        head.GetComponent<MaskSelectorScript>().SetMask(aClass);
    }

    private GameObject SendPredatorRetrieveActiveChild()
    {
        GameObject firstActiveGameObject = null;

        for (int i = 0; i < head.childCount; i++)
        {
            if (head.GetChild(i).gameObject.activeSelf == true)
            {
                firstActiveGameObject = head.GetChild(i).gameObject;
            }
        }
        return firstActiveGameObject;
    }


    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerClass = PlayerManager.CharacterClassesEnum.Melee;
            weapon = PlayerManager.GetWeapon(PlayerClass, gameObject);
            ability = PlayerManager.GetAbility(PlayerClass, gameObject);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerClass = PlayerManager.CharacterClassesEnum.Bowman;
            weapon = PlayerManager.GetWeapon(PlayerClass, gameObject);
            ability = PlayerManager.GetAbility(PlayerClass, gameObject);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayerClass = PlayerManager.CharacterClassesEnum.Magician;
            weapon = PlayerManager.GetWeapon(PlayerClass, gameObject);
            ability = PlayerManager.GetAbility(PlayerClass, gameObject);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayerClass = PlayerManager.CharacterClassesEnum.Dartblower;
            weapon = PlayerManager.GetWeapon(PlayerClass, gameObject);
            ability = PlayerManager.GetAbility(PlayerClass, gameObject);
        }
        
#endif
        movementSpeed = (originalMovementSpeed * (1 + (Stats.Agility / 100f)));

        if (Stats.HasStatus(Statuses.Slowed))
            movementSpeed = movementSpeed * 0.5f;

        if (Stats.HasStatus(Statuses.Bleeding))
        {
            Health -= 10 * Time.deltaTime;
        }

        bodyMesh.gameObject.SetActive(!Stats.HasStatus(Statuses.Invisible));
    }

    private void FixedUpdate()
    {
        if (PlayerManager.playerReady[playerID])
        {
            // Movement
            if (Input.GetButtonDown(Inputs.AButton(PlayerManager.controllerId[playerID])))
            {
                if (ability.CanUse)
                    ability.Use();
            }
            else if (Stats.CanMove)
            {
                Vector2 playerVelocity = new Vector2(Input.GetAxisRaw(Inputs.Horizontal(PlayerManager.controllerId[playerID])), Input.GetAxisRaw(Inputs.Vertical(PlayerManager.controllerId[playerID])));
                rb.velocity = playerVelocity.normalized * movementSpeed;
                anim.SetFloat(AnimatorConstants.Speed, playerVelocity.magnitude);

                if (rb.velocity.magnitude > DEADZONE)
                {
                    body.eulerAngles = new Vector3(body.eulerAngles.x, body.eulerAngles.y, (Mathf.Atan2(rb.velocity.x, rb.velocity.y) * 180 / Mathf.PI) + 180);
                }
            }

            // Attack
            Vector2 attackDirection = new Vector2(Input.GetAxisRaw(Inputs.FireHorizontal(PlayerManager.controllerId[playerID])), Input.GetAxisRaw(Inputs.FireVertical(PlayerManager.controllerId[playerID])));

            Vector2 attackPosition = transform.position;
            if (attackDirection.magnitude > DEADZONE)
            {
                head.eulerAngles = new Vector3(head.eulerAngles.x, head.eulerAngles.y, (Mathf.Atan2(attackDirection.y, attackDirection.x) * 180 / Mathf.PI) * -1 - 90);
                var attackRotation = new Vector3(0, 0, (Mathf.Atan2(attackDirection.y, attackDirection.x) * 180 / Mathf.PI) - 90);
                weapon.Attack(attackPosition, attackDirection, Quaternion.Euler(attackRotation), radius);
            }
        }
        else
        {
            if (!PlayerManager.playerReady[playerID])
            {
                PlayerManager.MapControllerToPlayer();
            }
        }
    }

    public override void TakeDamageExtender(float damage, GameObject sender, Collider2D collider)
    {
        // Currently don't give af
        // If class is X heal yourself and all around with 15% of damage
        var EvilDoer = (sender != null ? (sender.GetComponent<Unit>()) : null);
        var EvilDoerPortrait = (EvilDoer != null ? EvilDoer.Portrait : null);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Revive player on touch
        var player = collision.collider.GetComponent<Player>();

        if (player != null && player.IsDead)
        {
            player.Health = player.maxHealth / 2;
            SoundManager.instance.PlayAudio(14);
            ParticleSpawner.instance.SpawnParticleEffect(player.transform.position, Vector2.up, ParticleSpawner.ParticleTypes.Hit);
            PlayerManager.playerReady[player.playerID] = true;
            PlayerManager.playersReady += 1;
            collision.transform.rotation = transform.rotation;
            Rigidbody2D rigid = player.GetComponent<Rigidbody2D>();
            rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
