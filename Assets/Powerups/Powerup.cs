using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
public class Powerup : MonoBehaviour
{
    [Header("Attributes")]
    [Range(0, 50)]
    public int Agility;
    [Range(0, 50)]
    public int Intelligence;
    [Range(0, 50)]
    public int Strength;

    [Header("Status")]
    public bool ModifyStatus = false;
    public Statuses Status;

    [Header("Weapon")]
    public Weapon Weapon;

    [Header("Health")]
    [Range(0, 1000)]
    public float Health;
    [Range(0, 1000)]
    public float MaxHealth;

    [Header("Experience")]
    [Range(0, 500)]
    public int Experience;

    [Header("Duration")]
    [Tooltip("Leave unchecked if this is a permanent powerup")]
    public bool ShouldBeTemporary;
    [Range(0, 30)]
    [TextArea]
    public float Seconds;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player != null)
        {
            if (ModifyStatus)
                player.Stats.Status.Add(Status);

            if (Weapon != null)
                player.weapon = Weapon;

            player.Stats.Agility += Agility;
            player.Stats.Intelligence += Intelligence;
            player.Stats.Strength += Strength;

            player.Health += Health;
            player.maxHealth += MaxHealth;
        }
    }
}
