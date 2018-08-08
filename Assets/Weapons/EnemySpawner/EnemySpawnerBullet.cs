using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

class EnemySpawnerBullet : Projectile
{
    private static float lifetime = 1;
    private static float damage = 0;

    public GameObject SpawnedEnemy;
    public EnemySpawnerBullet() : base(lifetime, damage)
    {
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }
    

    private IEnumerator SpawnEnemies()
    {
        List<GameObject> spawn = new List<GameObject>();
        var pos = Owner.transform.position;
        yield return new WaitForSeconds(lifetime - 0.7f);
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
        // Set their target to Owners GetComponent Enemy target
        /*foreach (var obj in spawn)
        {
            // obj.GetComponent<Enemy>().target = Owner.GetComponent<Enemy>().target;
        }*/
        Destroy(gameObject);
    }
}
