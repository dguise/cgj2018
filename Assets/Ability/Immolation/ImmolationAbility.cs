using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ImmolationAbility : Ability
{
    protected override float Cooldown { get; set; }
    float duration;
    int amount;
    int max;

    public ImmolationAbility(GameObject go): base(go)
    {
        Cooldown = 15;
        duration = 5;
        amount = 3;
        max = 5;
    }


    public override IEnumerator ActuallyUse()
    {
        GameObject attackWeapon = Resources.Load<GameObject>("ImmolationBullet"); 
        Weapon immolation = new ImmolationGun(Owner);
        immolation.Attack(Owner.transform.position, Vector2.right, OwnerPlayer.transform.rotation, 0.5f);

        yield return null;
    }

    public Vector2 DFT(int i, int length) {
        float real = Mathf.Cos(2 * i * Mathf.PI / length);
        float imag = Mathf.Sin(2 * i * Mathf.PI / length);
        return new Vector2(real, imag);
    }

}
