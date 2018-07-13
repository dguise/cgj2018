using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeBullet : Projectile
{
    private static float speed = 0.1f;
    private static float lifetime = 0.15f;
    private static float damage = 30;
    private static int sound = 3;


    public PlayerMeleeBullet() : base(speed, lifetime, damage, sound)
    {
    }
}
