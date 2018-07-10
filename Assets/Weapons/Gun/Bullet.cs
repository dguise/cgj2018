using UnityEngine;

public class Bullet : Projectile
{
    private static float speed = 10;
    private static float lifetime = 3;
    private static float damage = 10;

    public Bullet() : base(speed, lifetime, damage)
    {

    }
}