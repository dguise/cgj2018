using UnityEngine;

public class EnemyBullet : Projectile
{
    private static float lifetime = 3;
    private static float damage = 10;
    private static int sound = 5;

    public EnemyBullet() : base(lifetime, damage, sound)
    {

    }
}