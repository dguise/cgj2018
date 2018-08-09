using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public abstract class Projectile : MonoBehaviour
{
    public float Lifetime { get; set; }
    public float Damage { get; set; }
    // Is set by Weapon.cs
    public GameObject Owner { get; set; }
    public bool destroyOnCollision = true;
    public int Sound { get; set; }

    public Projectile (float lifetime, float damage, int sound = 0)
    {
        this.Lifetime = lifetime;
        this.Damage = damage;
        this.Sound = sound;
    }

    private void Awake()
    {
		SoundManager sm = SoundManager.instance;
        sm.PlayRandomize(0.05f, Sound);
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
            Die();
        }
    }

    private void OnAnyCollide(Collider2D col)
    {
        if (destroyOnCollision || col.gameObject.layer == LayerConstants.GetLayer("Wall").value)
            Die();
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