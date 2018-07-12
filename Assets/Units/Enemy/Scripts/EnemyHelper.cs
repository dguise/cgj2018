using UnityEngine;
using System.Collections;
public enum EnemyClass
{
    RegularEnemy,
    EnemySpawner,
    Melee,
    SuicideCrawler,
}
public class EnemyHelper
{
    public static Weapon GetWeapon(EnemyClass enemyClass, GameObject go)
    {
        Weapon weapon = new Gun(go);

        switch (enemyClass)
        {
            case EnemyClass.EnemySpawner:
                weapon = new EnemySpawnerGun(go);
                break;
            case EnemyClass.RegularEnemy:
                weapon = new Gun(go);
                break;
            case EnemyClass.Melee:
                weapon = new MeleeGun(go);
                break;
            case EnemyClass.SuicideCrawler:
                weapon = new ExplodeSelfGun(go);
                break;

        }

        return weapon;
    }
}
