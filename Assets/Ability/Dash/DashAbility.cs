using UnityEngine;
using System.Collections;

public class DashAbility : Ability
{
    protected override float Cooldown { get; set; }
    float duration;

    public DashAbility(GameObject go): base (go)
    {
        Cooldown = 5;
        duration = 0.6f;
    }

    public override IEnumerator ActuallyUse()
    {
        OwnerUnit.Stats.SetStatus(OwnerUnit, duration, Statuses.Stunned, Statuses.Invincible);

        var rb = OwnerUnit.RigidBody;
        rb.velocity = Vector2.zero;

        rb.AddRelativeForce(-1 * (OwnerUnit.UnitBodyDirection * OwnerUnit.movementSpeed * 2), ForceMode2D.Impulse);

        yield return null;
    }
}
