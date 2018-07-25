using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeGun : Weapon
{
    protected override GameObject AttackWeapon { get; set; }
    protected override GameObject spawnAttack { get; set; }
    protected override float cooldown { get; set; }
    protected override float speed { get; set; }
    protected override float attackTimestamp { get; set; }

    public MeleeGun(GameObject go) : base(go)
    {
        AttackWeapon = PrefabRepository.instance.MeleeBullet;
        attackTimestamp = -(cooldown + 1);
        cooldown = 3f;
        speed = 4f;
    }
    private Transform from;
    private Transform to;
    public override GameObject Attack(Transform from, Transform to)
    {
        this.from = from;
        this.to = to;
        if (attackTimestamp + cooldown <= Time.time && !isAttacking)
        {
            owner.GetComponent<Animator>().SetTrigger("Attack");
            isAttacking = true;
        }
        return null;
    }
    public bool isAttacking = false;
    public void PerformAttack()
    {
        var atk = base.Attack(from, to);
        if (atk != null && owner != null)
            atk.GetComponent<CircleCollider2D>().radius = owner.GetComponent<CircleCollider2D>().radius;
        isAttacking = false;
    }
}
