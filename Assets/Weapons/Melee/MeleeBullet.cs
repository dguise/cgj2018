using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBullet : Projectile {
    private static float lifetime = 0.3f;
    private static float damage = 50;
    private static int sound = 3;


    public MeleeBullet() : base(lifetime, damage, sound)
    {
    }
}
