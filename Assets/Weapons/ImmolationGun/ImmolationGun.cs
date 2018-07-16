using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class ImmolationGun : Weapon
{
    protected override GameObject attackWeapon { get; set; }
    protected override GameObject spawnAttack { get; set; }
    protected override float cooldown { get; set; }
    protected override float speed { get; set; }
    protected override float attackTimestamp { get; set; }

    private int _bulletLimit;
    private int _maxN;
    private float _lifetime;
    List<GameObject> projectiles = new List<GameObject>();
    

    public ImmolationGun(GameObject owner, float cd = 0.0f, int bulletLimit = 4, int maxN = 4, float lifetime = 4) : base(owner)
    {
        attackWeapon = Resources.Load<GameObject>("ImmolationBullet");
        attackTimestamp = -(cooldown + 1);
        cooldown = cd;
        speed = 4f;
        _bulletLimit = bulletLimit;
        _maxN = maxN;
        _lifetime = lifetime;
    }

    public override GameObject Attack(Vector2 position, Vector2 direction, Quaternion rotation, float radius)
    {
        GameObject projectile = null;

        projectiles.RemoveAll(x => x == null);

        if (projectiles.Count < _bulletLimit)
        {
            for (int i = 0; i < _bulletLimit; i++) {
                projectile = base.Attack(position, direction, rotation, radius);

                if (projectile != null)
                {
                    projectile.GetComponent<Projectile>().destroyOnCollision = false;
                    ImmolationBullet bullet = projectile.GetComponent<ImmolationBullet>();
                    bullet.Rotate(MMath.DFT(i, _maxN));
                    bullet.Invoke("Die", _lifetime);
                }
            }
        }

        if (projectile != null)
            projectiles.Add(projectile);

        return projectile;
    }
}
