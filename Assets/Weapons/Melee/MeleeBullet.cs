using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBullet : Projectile {
    private static float speed = 20;
    private static float lifetime = 0.1f;
    private static float damage = 50;


    public MeleeBullet() : base(speed, lifetime, damage)
    {
    }
}
