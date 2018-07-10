using UnityEngine;

class ShieldGun : Weapon
{
    protected override GameObject attackWeapon { get; set; }
    protected override GameObject spawnAttack { get; set; }
    protected override float cooldown { get; set; }
    protected override float speed { get; set; }
    protected override float attackTimestamp { get; set; }

    public ShieldGun(GameObject owner): base (owner)
    {
        attackWeapon = Resources.Load<GameObject>("ShieldBullet");
        attackTimestamp= -(cooldown + 1);
        cooldown = 0.2f;
        speed = 4f;
    }

    public override GameObject Attack(Vector2 position, Vector2 direction, Quaternion rotation, float radius)
    {
        var projectile = base.Attack(position, direction, rotation, radius);
        return projectile;
    }
}