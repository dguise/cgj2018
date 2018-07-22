using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public static ParticleSpawner instance = null;

    public enum ParticleTypes
    {
        YellowPixelExplosion,
        BloodParticles,
        LevelUp,
        SiphonBloodAbility,
        BlueGlitter_OverDistance,
        BlueGlitter_OverTime,
        BluePixelTrail,
        BluePixelTrail_Fewer,
        BluePixelTrail_Streched_Fast,
        Fire,
        Footsteps,
        RedPixelExplosion_Up,
        StaticFire,
    }

    // No usage, just a quick reference in the Inspector to see what order the particles should be entered at
    [SerializeField]
    private ParticleTypes _listIndexReference;
    public List<GameObject> particleEffects;

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

    public void SpawnParticleEffect(Vector2 where, Vector2 direction, ParticleTypes effect)
    {
        GameObject particleEffect = particleEffects[(int)effect];
        float lifetime = 2.0f;

        GameObject.Destroy(Instantiate(particleEffect, (Vector3)where, Quaternion.Euler(direction)), lifetime);

    }
}
