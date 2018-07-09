using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public abstract class Projectile : MonoBehaviour
{
    public abstract float Speed { get; set; }
    public abstract float Lifetime { get; set; }
    public abstract float Damage { get; set; }

    private void Start()
    {
        Invoke("Die", Lifetime);
    }
    
    void Die()
    {
        Destroy(this);
    }

    private void OnCollisionEnter2D(Collision collision)
    {
        var unit = collision.collider.GetComponent<Unit>();
        if (unit != null)
        {
            unit.TakeDamage(Damage);
        }
    }
}