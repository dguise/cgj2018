using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

class EnemySpawnerBullet : Projectile
{
    private static float speed = 10;
    private static float lifetime = 1;
    private static float damage = 0;

#pragma warning disable 0649
    public GameObject SpawnedEnemy;
#pragma warning restore 0649
    public EnemySpawnerBullet() : base(speed, lifetime, damage)
    {
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }
    

    private IEnumerator SpawnEnemies()
    {
        List<GameObject> spawn = new List<GameObject>();
        yield return new WaitForSeconds(lifetime - 0.7f);
        var pos = Owner.transform.position;
        pos.x -= 1;
        spawn.Add(Instantiate(SpawnedEnemy, pos, SpawnedEnemy.transform.rotation));
        yield return new WaitForSeconds(0.1f);
        pos.x += 1;
        pos.y -= 1;
        spawn.Add(Instantiate(SpawnedEnemy, pos, SpawnedEnemy.transform.rotation));
        yield return new WaitForSeconds(0.1f);
        pos.x += 1;
        pos.y += 1;
        spawn.Add(Instantiate(SpawnedEnemy, pos, SpawnedEnemy.transform.rotation));
        yield return new WaitForSeconds(0.1f);
        pos.x -= 1;
        pos.y += 1;
        spawn.Add(Instantiate(SpawnedEnemy, pos, SpawnedEnemy.transform.rotation));

        foreach (var obj in spawn)
        {
            // obj.GetComponent<Enemy>().target = Owner.GetComponent<Enemy>().target;
        }
        Destroy(gameObject);
        // Set their target to Owners GetComponent Enemy target
    }
}
