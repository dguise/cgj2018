﻿using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public abstract class Projectile : MonoBehaviour
{
    public float Speed { get; set; }
    public float Lifetime { get; set; }
    public float Damage { get; set; }
    // Is set by Weapon.cs
    public GameObject Owner { get; set; }

    public Projectile (float speed, float lifetime, float damage)
    {
        this.Speed = speed;
        this.Lifetime = lifetime;
        this.Damage = damage;
    }

    private void Start()
    {
        Invoke("Die", Lifetime);
    }

    void Die()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var unit = collision.collider.GetComponent<Unit>();

        if (unit != null)
        {
            unit.TakeDamage(Damage, Owner, collision.collider);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var unit = collider.GetComponent<Unit>();

        if (unit != null)
        {
            unit.TakeDamage(Damage, Owner, collider);
            Destroy(gameObject);
        }
    }
}