using UnityEngine;
using System.Collections;

public class EnemySpawnerGun : Weapon
{
    protected override GameObject attackWeapon { get; set; }
    protected override GameObject spawnAttack { get; set; }
    protected override float cooldown { get; set; }
    protected override float speed { get; set; }
    protected override float attackTimestamp { get; set; }

    public EnemySpawnerGun(GameObject owner) : base(owner)
    {
        attackWeapon = Resources.Load<GameObject>("EnemySpawnerBullet");
        attackTimestamp = Time.time + cooldown;
        cooldown = 5;
        speed = 2f;
    }
}
