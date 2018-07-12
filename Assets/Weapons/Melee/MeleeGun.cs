using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeGun : Weapon
{
    protected override GameObject attackWeapon { get; set; }
    protected override GameObject spawnAttack { get; set; }
    protected override float cooldown { get; set; }
    protected override float speed { get; set; }
    protected override float attackTimestamp { get; set; }

    public MeleeGun(GameObject go) : base(go)
    {
        attackWeapon = Resources.Load<GameObject>("MeleeBullet");
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
        base.Attack(from, to);
        isAttacking = false;
    }
}
