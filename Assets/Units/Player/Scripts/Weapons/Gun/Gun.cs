using UnityEngine;

class Gun : IWeapon {
	public GameObject attackWeapon;
    GameObject spawnAttack;
	float cooldown = 0.2f;
	float speed = 4f;
	float lastAttack;

    public Gun()
	{
		attackWeapon = Resources.Load<GameObject>("Bullet");
		lastAttack = -(cooldown + 1) ;
	}

	public void Attack(Vector2 position, Vector2 direction, Quaternion rotation, float radius) {
		var currentTime = Time.time;
		if (lastAttack + cooldown < currentTime) 
		{
			lastAttack = currentTime;
			spawnAttack = MonoBehaviour.Instantiate(attackWeapon, position + direction * radius, rotation);
			spawnAttack.GetComponent<Rigidbody2D>().velocity = direction * speed;
		}
    }
}