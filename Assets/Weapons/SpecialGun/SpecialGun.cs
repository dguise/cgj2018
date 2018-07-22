using UnityEngine;

class SpecialGun : Weapon
{
    protected override GameObject AttackWeapon { get; set; }
    protected override GameObject spawnAttack { get; set; }
    protected override float cooldown { get; set; }
    protected override float speed { get; set; }
    protected override float attackTimestamp { get; set; }

    public SpecialGun(GameObject owner, float cd = 0.2f): base (owner)
    {
        AttackWeapon = Resources.Load<GameObject>("SpecialBullet");
        attackTimestamp= -(cooldown + 1);
        cooldown = cd;
        speed = 4f;
    }

    public override GameObject Attack(Vector2 position, Vector2 direction, Quaternion rotation, float radius)
    {
        var projectile = base.Attack(position, direction, rotation, radius);
        if (projectile != null)
        {
            projectile.GetComponent<SpecialBullet>().SetStartAndDirectionVectorAndSpeed(position + direction * radius, direction, speed);
        }
        return projectile;
    }
}