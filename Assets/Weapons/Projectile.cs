using UnityEngine;

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
        OnAnyCollide(collision.collider);
        var unit = collision.collider.GetComponent<Unit>();

        if (unit != null)
        {
            unit.TakeDamage(Damage, Owner, collision.collider);
            Destroy(gameObject);
        }
    }

    private void OnAnyCollide(Collider2D col)
    {
        if (col.IsTouchingLayers(LayerConstants.GetOnlyLayer("Enemies")))
        {
            Debug.Log("Collided with Enemies");
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        OnAnyCollide(collider);
        var unit = collider.GetComponent<Unit>();

        if (unit != null)
        {
            unit.TakeDamage(Damage, Owner, collider);
        }
    }
}