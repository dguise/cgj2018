using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public static ParticleSpawner instance = null;

    public enum ParticleTypes
    {
        Hit,
        Blood,
        Death,
        SomethingElse
    }
    public List<GameObject> List_Of_ParticleEffects;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        //DontDestroyOnLoad(gameObject);
        //SpawnParticleEffect(this.transform.position, Vector2.zero, ParticleTypes.Hit);
    }

    //void Update()
    //{
    //    float rng = Random.Range(0, 100000);
    //    if (rng > 90000)
    //        if (Random.Range(0, 10) > 5)
    //            SpawnParticleEffect(this.transform.position, Vector2.zero, ParticleTypes.Hit);
    //        else
    //            SpawnParticleEffect(this.transform.position, Vector2.zero, ParticleTypes.Blood);
    //}


    public void SpawnParticleEffect(Vector2 where, Vector2 direction, ParticleTypes effect)
    {
        GameObject particleEffect = List_Of_ParticleEffects[(int)effect];
        float lifetime = 2.0f;

        GameObject.Destroy(Instantiate(particleEffect, (Vector3)where, Quaternion.Euler(direction)), lifetime);

    }
}
