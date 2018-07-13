using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class Weapon : IWeapon
{
    protected abstract GameObject attackWeapon { get; set; }
    protected abstract GameObject spawnAttack { get; set; }
    protected abstract float cooldown { get; set; }
    protected abstract float speed { get; set; }
    protected abstract float attackTimestamp { get; set; }

    protected GameObject owner;
    protected Unit ownerUnit;
    protected float radius;

    public Weapon(GameObject owner)
    {
        this.owner = owner;
        this.ownerUnit = owner.GetComponent<Unit>();
        radius = owner.GetComponent<CircleCollider2D>().radius;
        
    }
    public virtual GameObject Attack(Transform from, Transform towards)
    {
        if (from != null && towards != null)
            return Attack(from.transform.position, towards.transform.position - from.transform.position, Quaternion.identity, radius);

        return null;
    }

    public virtual GameObject Attack(Vector2 position, Vector2 direction, Quaternion rotation, float radius)
    {
        var currentTime = Time.time;
        direction = direction.normalized;
        if (attackTimestamp + cooldown <= currentTime)
        {
            attackTimestamp = currentTime;
            spawnAttack = MonoBehaviour.Instantiate(attackWeapon, position + direction * radius, rotation);
            spawnAttack.GetComponent<Rigidbody2D>().velocity = direction * speed;
            var projectile = spawnAttack.GetComponent<Projectile>();
            projectile.Owner = owner;

            // Stat modifier
            projectile.Damage = projectile.Damage + ownerUnit.Stats.Strength;
            if (Random.Range(0f, 1f) > ownerUnit.Stats.Intelligence / 100)
                projectile.Damage = projectile.Damage * 2;

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
