using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

class EnemySpawnerBullet : Projectile
{
    private static float speed = 10;
    private static float lifetime = 1;
    private static float damage = 0;

    public GameObject SpawnedEnemy;

    public EnemySpawnerBullet() : base(speed, lifetime, damage)
    {
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }
    

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(lifetime - 0.7f);
        var pos = transform.position;
        pos.x -= 1;
        Instantiate(SpawnedEnemy, pos, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        pos.x += 1;
        pos.y -= 1;
        Instantiate(SpawnedEnemy, pos, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        pos.x += 1;
        pos.y += 1;
        Instantiate(SpawnedEnemy, pos, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        pos.x -= 1;
        pos.y += 1;
        Instantiate(SpawnedEnemy, pos, Quaternion.identity);
        Destroy(gameObject);
        // Set their target to Owners GetComponent Enemy target
    }
}
