using UnityEngine;

public class Bullet : Projectile
{
    private static float lifetime = 3;
    private static float damage = 10;
    private static int sound = 5;

    public Bullet() : base(lifetime, damage, sound)
    {

    }
}