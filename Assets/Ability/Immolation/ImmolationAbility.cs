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
        //Weapon tempWeapon = OwnerPlayer.weapon;
        //OwnerPlayer.weapon = new ImmolationGun(Owner);
        //OwnerPlayer.weapon.Attack(Owner.transform.position, Vector2.right, OwnerPlayer.transform.rotation, 0.5f);
        //OwnerPlayer.weapon = tempWeapon;
        //
        Weapon immolation = new ImmolationGun(Owner);
        immolation.Attack(Owner.transform.position, Vector2.right, OwnerPlayer.transform.rotation, 0.5f);


        //List<GameObject> projectiles = new List<GameObject>();

        /*for (int i = 0; i < amount; i++) {
            GameObject projectile = MonoBehaviour.Instantiate(attackWeapon, Owner.transform.position, Owner.transform.rotation);
            if (projectile != null) {
                //Special version of weapon attack, since a weapon
                projectile.GetComponent<Projectile>().destroyOnCollision = false;
                ImmolationBullet bullet = projectile.GetComponent<ImmolationBullet>();
                bullet.Rotate(DFT(i, max));
                bullet.Invoke("Die", bullet.Lifetime);
                bullet.Owner = Owner;
                projectiles.Add(projectile);

                if (owner.tag == Tags.Player)
                {
                    spawnAttack.layer = LayerConstants.GetLayer(LayerConstants.PlayerProjectiles);
                } else
                {
                    spawnAttack.layer = LayerConstants.GetLayer(LayerConstants.EnemyProjectiles);
                }
            }
        }*/

        yield return null;
    }

    public Vector2 DFT(int i, int length) {
        float real = Mathf.Cos(2 * i * Mathf.PI / length);
        float imag = Mathf.Sin(2 * i * Mathf.PI / length);
        return new Vector2(real, imag);
    }

}
