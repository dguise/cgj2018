using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Player : Unit
{
    public Weapon weapon;
    float radius = 0.5f;
    const float DEADZONE = 0.98f;
    public int playerID;
    Animator anim;

    Transform head;
    Transform body;

    Rigidbody2D rb;

    private bool canDash = true;
    private bool dashing = false;
    private float dashTime = 0.5f;
    private float dashResetTime
    {
        get { return dashTime * 5; }
    }

    public PlayerManager.CharacterClassesEnum PlayerClass;

    void Start()
    {
        weapon = PlayerManager.GetWeapon(PlayerClass, gameObject);

        head = transform.Find("monkeyhead");
        body = transform.Find("Armature.001");

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

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
            weapon = new Gun(gameObject);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            weapon = new SpecialGun(gameObject);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            weapon = new ShieldGun(gameObject);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            weapon = new EnemySpawnerGun(gameObject);

        //Debug.Log(Stats.Status.IndexOf(Statuses.Invisible));

    }

    private void FixedUpdate()
    {
        if (PlayerManager.playerReady[playerID])
        {
            // Movement
            if (Input.GetButtonDown(Inputs.AButton(playerID + 1)))
            {
                if (canDash)
                    StartCoroutine(Dash());
            }
            else if (!dashing)
            {
                Vector2 playerVelocity = new Vector2(Input.GetAxisRaw(Inputs.Horizontal(PlayerManager.controllerId[playerID])), Input.GetAxisRaw(Inputs.Vertical(PlayerManager.controllerId[playerID])));
                rb.velocity = playerVelocity.normalized * (movementSpeed * 1 + (Stats.Agility / 50));
                anim.SetFloat(AnimatorConstants.Speed, playerVelocity.magnitude);

                if (rb.velocity.magnitude > DEADZONE)
                {
                    body.eulerAngles = new Vector3(body.eulerAngles.x, body.eulerAngles.y, (Mathf.Atan2(rb.velocity.x, rb.velocity.y) * 180 / Mathf.PI) + 180);
                }
            }

            // Attack
            Vector2 attackDirection = new Vector2(Input.GetAxisRaw(Inputs.FireHorizontal(PlayerManager.controllerId[playerID])), Input.GetAxisRaw(Inputs.FireVertical(PlayerManager.controllerId[playerID])));

            Vector2 attackPosition = transform.position;
            Quaternion attackRotation = Quaternion.identity;
            if (attackDirection.magnitude > DEADZONE)
            {
                weapon.Attack(attackPosition, attackDirection, attackRotation, radius);
                head.eulerAngles = new Vector3(head.eulerAngles.x, head.eulerAngles.y, (Mathf.Atan2(attackDirection.y, attackDirection.x) * 180 / Mathf.PI) * -1 - 90);
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

    IEnumerator Dash()
    {
        dashing = true;
        canDash = false;

        Vector2 lastDirection = rb.velocity.normalized;

        rb.velocity = Vector2.zero;

        rb.AddRelativeForce(-1 * (lastDirection * movementSpeed * 2), ForceMode2D.Impulse);
        anim.SetFloat(AnimatorConstants.Speed, rb.velocity.magnitude);

        Invoke("EnableDash", dashResetTime);
        yield return new WaitForSeconds(dashTime);
        dashing = false;
    }

    void EnableDash()
    {
        canDash = true;
    }

    public override void TakeDamageExtender(float damage, GameObject sender, Collider2D collider)
    {
        // Currently don't give af
        // If class is X heal yourself and all around with 15% of damage
        var EvilDoer = (sender != null ? (sender.GetComponent<Unit>()) : null);
        var EvilDoerPortrait = (EvilDoer != null ? EvilDoer.Portrait : null);
        GuiScript.instance.Talk(new Message(this.Portrait, EvilDoerPortrait, "aj aj aj", Message.MessagetypeEnum.QuickMessageAllAtOnce ));
    }
}
