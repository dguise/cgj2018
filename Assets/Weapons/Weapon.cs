using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class Weapon : IWeapon
{
    protected abstract GameObject AttackWeapon { get; set; }
    protected abstract GameObject spawnAttack { get; set; }
    protected abstract float cooldown { get; set; }
    protected abstract float speed { get; set; }
    protected abstract float attackTimestamp { get; set; }

    protected GameObject owner;
    protected Unit ownerUnit;
    protected float radius;

    public int projectiles = 1;
    public int maxProjectiles = 1;
    private float projectileWidthInDegrees = 15;

    public Weapon(GameObject owner)
    {
        this.owner = owner;
        this.ownerUnit = owner.GetComponent<Unit>();
        radius = owner.GetComponent<CircleCollider2D>().radius;

    }

    public virtual GameObject Attack(Transform from, Vector3 towards)
    {
        if (from != null && towards != null)
            return Attack(from.transform.position, towards - from.transform.position, Quaternion.identity, radius);

        return null;
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
        var attackDirection = direction;
        if (attackTimestamp + cooldown <= currentTime)
        {
            attackTimestamp = currentTime;

            if (projectiles > 1)
                attackDirection = direction.MaakepRotate(-(projectileWidthInDegrees * projectiles / 2));

            for (int i = 0; i < projectiles; i++)
            {
                spawnAttack = MonoBehaviour.Instantiate(AttackWeapon, position + direction * radius, rotation);
                spawnAttack.GetComponent<Rigidbody2D>().velocity = attackDirection * speed;
                var projectile = spawnAttack.GetComponent<Projectile>();
                projectile.Owner = owner;

                // Stat modifier
                projectile.Damage = projectile.Damage + ownerUnit.Stats.Strength;
                if (ownerUnit.Stats.Intelligence / 100 > Random.Range(0f, 1f))
                    projectile.Damage = projectile.Damage * 2;

                if (owner.tag == Tags.Player)
                {
                    spawnAttack.layer = LayerConstants.GetLayer(LayerConstants.PlayerProjectiles);
                }
                else
                {
                    spawnAttack.layer = LayerConstants.GetLayer(LayerConstants.EnemyProjectiles);
                }

                // For next projectile
                attackDirection = attackDirection.MaakepRotate(projectileWidthInDegrees);
            }
            return spawnAttack;
        }
        return null;
    }
}
