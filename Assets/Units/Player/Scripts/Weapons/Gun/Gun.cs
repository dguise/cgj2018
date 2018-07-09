using UnityEngine;

class Gun : IWeapon {
	public GameObject attackWeapon;
    GameObject spawnAttack;

    public Gun()
	{
		attackWeapon = Resources.Load<GameObject>("Bullet");
	}

	public void Attack(Vector2 position, Vector2 direction, Quaternion rotation, float radius) {
		spawnAttack = MonoBehaviour.Instantiate(attackWeapon, position + direction * radius, rotation);
		spawnAttack.GetComponent<Rigidbody2D>().velocity = direction;
    }
}