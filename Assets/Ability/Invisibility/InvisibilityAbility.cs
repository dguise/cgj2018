using UnityEngine;
using System.Collections;

public class InvisibilityAbility : Ability
{
    protected override float Cooldown { get; set; }
    float duration;

    public InvisibilityAbility(GameObject go): base(go)
    {
        Cooldown = 10;
        duration = 4;
    }


    public override IEnumerator ActuallyUse()
    {
        PowerupObject po = new PowerupObject
        {
            Agility = 50,
            ModifyStatus = true,
            Status = Statuses.Invisible,
            ShouldBeTemporary = true,
            Seconds = duration,
        };
        po.HandlePowerup(po, (Player)OwnerUnit);
        yield return null;
    }
}
