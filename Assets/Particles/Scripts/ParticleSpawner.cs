using System.Collections.Generic;
using UnityEngine;

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

public class ParticleSpawner : MonoBehaviour
{
    public static ParticleSpawner instance = null;


    // No usage, just a quick reference in the Inspector to see what order the particles should be entered at
    // Would be nice if this was a serialized dictionary.
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

    public void SpawnParticleEffect(Vector2 where, ParticleTypes effect, Vector2? direction = null, Transform parent = null, float lifetime = 0, bool keepAlive = false)
    {
        Vector2 dir = Vector2.zero;

        if (direction.HasValue)
            dir = direction.Value;

        GameObject particleEffect = particleEffects[(int)effect];
        var particle = Instantiate(particleEffect, where, Quaternion.Euler(dir));

        if (parent != null)
            particle.transform.parent = parent;

        if (lifetime > 0)
            Destroy(particle, lifetime);
    }
}
