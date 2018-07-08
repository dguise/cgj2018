using UnityEngine;

class Bullet : IWeapon {
	public GameObject attackWeapon;

	public Bullet()
	{
		attackWeapon = Resources.Load<GameObject>("Bullet");
	}

	public void Attack(Vector2 position, Vector2 direction, Quaternion rotation, float radius) {
		GameObject spawnAttack = MonoBehaviour.Instantiate(attackWeapon, position + direction * radius, rotation);
		spawnAttack.GetComponent<Rigidbody2D>().velocity = direction;
	}
}