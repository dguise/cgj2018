using UnityEngine;

class EnemyGun : Weapon {
	protected override GameObject AttackWeapon { get; set; }
    protected override GameObject spawnAttack { get; set; }
    protected override float cooldown { get; set; }
    protected override float speed { get; set; }
    protected override float attackTimestamp { get; set; }

    public EnemyGun(GameObject owner, float cd = 3f, int projs = 1, float spd = 4f): base(owner)
	{
        AttackWeapon = PrefabRepository.instance.EnemyBullet;
		attackTimestamp = -(cooldown + 1);
        cooldown = cd;
        speed = spd;
        maxProjectiles = 365;
        projectiles = projs;
	}
}