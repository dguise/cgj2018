using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeGun : Weapon
{
    protected override GameObject AttackWeapon { get; set; }
    protected override GameObject spawnAttack { get; set; }
    protected override float cooldown { get; set; }
    protected override float speed { get; set; }
    protected override float attackTimestamp { get; set; }

    public PlayerMeleeGun(GameObject go) : base(go)
    {
        AttackWeapon = Resources.Load<GameObject>("PlayerMeleeBullet");
        attackTimestamp = -(cooldown + 1);
        cooldown = 0.5f;
        speed = 1f;
    }
}
