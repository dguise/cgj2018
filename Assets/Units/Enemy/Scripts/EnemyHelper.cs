using UnityEngine;

public enum EnemyClass
{
    RegularEnemy,
    EnemySpawner,
    Melee,
    SuicideCrawler,
    SpeedyRange,
    ShieldImp,
    ShotgunEnemy,
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
                weapon = new EnemyGun(go, 4);
                break;
            case EnemyClass.Melee:
                weapon = new MeleeGun(go);
                break;
            case EnemyClass.SuicideCrawler:
                weapon = new ExplodeSelfGun(go);
                break;
            case EnemyClass.SpeedyRange:
                weapon = new EnemyGun(go, 1f);
                break;
            case EnemyClass.ShieldImp:
                weapon = new EnemyGun(go, 5, 30, 2);
                break;
            case EnemyClass.ShotgunEnemy:
                weapon = new EnemyGun(go, 3, 5);
                break;

        }

        return weapon;
    }
}
