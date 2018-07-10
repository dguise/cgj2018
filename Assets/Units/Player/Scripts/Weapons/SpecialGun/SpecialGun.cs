using UnityEngine;

class SpecialGun : IWeapon
{
    public GameObject attackWeapon;
    GameObject spawnAttack;
    float cooldown = 0.2f;
    float speed = 4f;
    float lastAttack;
    GameObject owner;

    public SpecialGun(GameObject owner)
    {
        attackWeapon = Resources.Load<GameObject>("SpecialBullet");
        lastAttack = -(cooldown + 1);
        this.owner = owner;
    }

    public void Attack(Vector2 position, Vector2 direction, Quaternion rotation, float radius)
    {
        var currentTime = Time.time;
        if (lastAttack + cooldown < currentTime)
        {
            lastAttack = currentTime;
            spawnAttack = MonoBehaviour.Instantiate(attackWeapon, position + direction * radius, rotation);
            spawnAttack.GetComponent<SpecialBullet>().SetStartAndDirectionVectorAndSpeed(position + direction * radius, direction, speed);
            spawnAttack.GetComponent<Projectile>().Owner = owner;
            Physics2D.IgnoreCollision(owner.GetComponent<Collider2D>(), spawnAttack.GetComponent<Collider2D>());
            //spawnAttack.GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
    }
}