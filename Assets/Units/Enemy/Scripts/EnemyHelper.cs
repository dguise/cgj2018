using UnityEngine;

public enum EnemyClass
{
    RegularEnemy,
    EnemySpawner,
    Melee,
    SuicideCrawler,
    SpeedyRange,
    ShieldImp,
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
            case EnemyClass.SpeedyRange:
                weapon = new SpecialGun(go, 1f);
                break;
            case EnemyClass.ShieldImp:
                weapon = new ShieldGun(go, 0.8f, 10);
                break;

        }

        return weapon;
    }
}
