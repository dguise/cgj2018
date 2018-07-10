using UnityEngine;

class Gun : Weapon {
	protected override GameObject attackWeapon { get; set; }
    protected override GameObject spawnAttack { get; set; }
    protected override float cooldown { get; set; }
    protected override float speed { get; set; }
    protected override float attackTimestamp { get; set; }

    public Gun(GameObject owner): base(owner)
	{
		attackWeapon = Resources.Load<GameObject>("Bullet");
		attackTimestamp = -(cooldown + 1);
        cooldown = 0.2f;
        speed = 4f;
	}
}