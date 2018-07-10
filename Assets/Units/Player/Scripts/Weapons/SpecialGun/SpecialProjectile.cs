using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public abstract class SpecialProjectile : MonoBehaviour
{
    public float Speed { get; set; }
    public float Lifetime { get; set; }
    public float Damage { get; set; }

    public SpecialProjectile (float speed, float lifetime, float damage)
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
        Debug.Log("Unit hit?");
        if (unit != null)
        {
            unit.TakeDamage(Damage, collision);
            Destroy(gameObject);
        }
    }
}