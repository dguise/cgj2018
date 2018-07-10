using UnityEngine;
using System.Collections;

public abstract class Weapon : IWeapon
{
    protected abstract GameObject attackWeapon { get; set; }
    protected abstract GameObject spawnAttack { get; set; }
    protected abstract float cooldown { get; set; }
    protected abstract float speed { get; set; }
    protected abstract float attackTimestamp { get; set; }

    GameObject owner;

    public Weapon(GameObject owner)
    {
        this.owner = owner;
    }
    public void Attack(Transform from, Transform towards)
    {
        Attack(from.transform.position, towards.transform.position - from.transform.position, Quaternion.identity, from.GetComponent<CircleCollider2D>().radius);
    }

    public void Attack(Vector2 position, Vector2 direction, Quaternion rotation, float radius)
    {
        var currentTime = Time.time;
        direction = direction.normalized;
        if (attackTimestamp + cooldown < currentTime)
        {
            attackTimestamp = currentTime;
            spawnAttack = MonoBehaviour.Instantiate(attackWeapon, position + direction * radius, rotation);
            spawnAttack.GetComponent<Rigidbody2D>().velocity = direction * speed;
            spawnAttack.GetComponent<Projectile>().Owner = owner;
            Physics2D.IgnoreCollision(owner.GetComponent<Collider2D>(), spawnAttack.GetComponent<Collider2D>());
        }
    }
}
