using UnityEngine;
using System.Collections;
public enum EnemyClass
{
    RegularEnemy,
    EnemySpawner,
    Melee,
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

        }

        return weapon;
    }
}
