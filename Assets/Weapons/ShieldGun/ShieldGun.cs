using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class ShieldGun : Weapon
{
    protected override GameObject attackWeapon { get; set; }
    protected override GameObject spawnAttack { get; set; }
    protected override float cooldown { get; set; }
    protected override float speed { get; set; }
    protected override float attackTimestamp { get; set; }

    private int _bulletLimit = 4;
    List<GameObject> projectiles = new List<GameObject>();

    public ShieldGun(GameObject owner) : base(owner)
    {
        attackWeapon = Resources.Load<GameObject>("ShieldBullet");
        attackTimestamp = -(cooldown + 1);
        cooldown = 0.2f;
        speed = 4f;
    }

    public override GameObject Attack(Vector2 position, Vector2 direction, Quaternion rotation, float radius)
    {
        GameObject projectile = null;

        projectiles.RemoveAll(x => x == null);

        if (projectiles.Count < _bulletLimit)
        {
            projectile = base.Attack(position, direction, rotation, radius);
        }
        else
        {
            foreach (var proj in projectiles)
            {
                proj.GetComponent<ShieldBullet>().ShouldCirculate = false;
                proj.GetComponent<Rigidbody2D>().velocity = direction.normalized * speed;
            }
            projectiles.Clear();
        }

        if (projectile != null)
            projectiles.Add(projectile);

        return projectile;
    }
}