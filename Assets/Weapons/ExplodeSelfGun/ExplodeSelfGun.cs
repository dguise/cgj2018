using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeSelfGun : Weapon
{
    protected override GameObject AttackWeapon { get; set; }
    protected override GameObject spawnAttack { get; set; }
    protected override float cooldown { get; set; }
    protected override float speed { get; set; }
    protected override float attackTimestamp { get; set; }

    float attackDamage = 30;
    float selfDamage = 50;

    public ExplodeSelfGun(GameObject go) : base(go)
    {
        cooldown = 3f;
        attackTimestamp = -(cooldown + 1);
    }

    public override GameObject Attack(Transform from, Transform to)
    {
        var hits = Physics2D.OverlapCircleAll(from.position, 4, LayerMask.GetMask("Enemies", "Players"));
        foreach (var hit in hits)
        {
            var unit = hit.GetComponent<Unit>();
            if (unit == null) continue;

            unit.TakeDamage(attackDamage, owner, owner.GetComponent<Collider2D>());
        }
        owner.GetComponent<Unit>().Health -= selfDamage;
        return null;
    }
}
