using UnityEngine;
using System.Collections;

public class DashAbility : Ability
{
    protected override float Cooldown { get; set; }
    float duration;

    public DashAbility(GameObject go): base (go)
    {
        Cooldown = 5;
        duration = 1;
    }

    public override IEnumerator ActuallyUse()
    {
        OwnerPlayer.Stats.SetStatus(OwnerPlayer, duration, Statuses.Stunned);

        var rb = OwnerPlayer.rb;
        Vector2 lastDirection = rb.velocity.normalized;

        rb.velocity = Vector2.zero;

        rb.AddRelativeForce(-1 * (lastDirection * OwnerPlayer.movementSpeed * 2), ForceMode2D.Impulse);
        OwnerPlayer.anim.SetFloat(AnimatorConstants.Speed, rb.velocity.magnitude);

        yield return null;
    }
}
