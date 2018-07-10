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
    float radius;

    public Weapon(GameObject owner)
    {
        this.owner = owner;
        radius = owner.GetComponent<CircleCollider2D>().radius;
        
    }
    public void Attack(Transform from, Transform towards)
    {
        Attack(from.transform.position, towards.transform.position - from.transform.position, Quaternion.identity, radius);
    }

    public virtual GameObject Attack(Vector2 position, Vector2 direction, Quaternion rotation, float radius)
    {
        var currentTime = Time.time;
        direction = direction.normalized;
        if (attackTimestamp + cooldown < currentTime)
        {
            attackTimestamp = currentTime;
            spawnAttack = MonoBehaviour.Instantiate(attackWeapon, position + direction * radius, rotation);
            spawnAttack.GetComponent<Rigidbody2D>().velocity = direction * speed;
            spawnAttack.GetComponent<Projectile>().Owner = owner;
            if (owner.tag == Tags.Player)
            {
                spawnAttack.layer = LayerConstants.GetLayer(LayerConstants.PlayerProjectiles);
            } else
            {
                spawnAttack.layer = LayerConstants.GetLayer(LayerConstants.EnemyProjectiles);
            }
            return spawnAttack;
        }
        return null;
    }
}
