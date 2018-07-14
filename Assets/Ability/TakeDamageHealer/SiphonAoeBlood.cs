using UnityEngine;
using System.Collections;

public class SiphonAoeBlood : Ability
{

    protected override float Cooldown { get; set; }
    float damage;
    float healRatio;
    float radius;

    public SiphonAoeBlood(GameObject go) : base(go)
    {
        Cooldown = 10;
        damage = 30;
        healRatio = 0.5f;
        radius = 5;
    }


    public override IEnumerator ActuallyUse()
    {
        var hits = Physics2D.OverlapCircleAll(Owner.transform.position, radius, LayerMask.GetMask("Enemies"));
        foreach(var hit in hits)
        {
            OwnerPlayer.TakeDamage(-(damage * healRatio), hit.gameObject, hit);
            hit.GetComponent<Unit>().TakeDamage(damage, Owner, hit);
            ParticleSpawner.instance.SpawnParticleEffect(hit.transform.position, Vector2.zero, ParticleSpawner.ParticleTypes.Blood);
            ParticleSpawner.instance.SpawnParticleEffect(hit.transform.position, Vector2.zero, ParticleSpawner.ParticleTypes.Blood);
            ParticleSpawner.instance.SpawnParticleEffect(hit.transform.position, Vector2.zero, ParticleSpawner.ParticleTypes.Blood);
            ParticleSpawner.instance.SpawnParticleEffect(hit.transform.position, Vector2.zero, ParticleSpawner.ParticleTypes.Blood);
            ParticleSpawner.instance.SpawnParticleEffect(hit.transform.position, Vector2.zero, ParticleSpawner.ParticleTypes.Blood);
            ParticleSpawner.instance.SpawnParticleEffect(hit.transform.position, Vector2.zero, ParticleSpawner.ParticleTypes.Blood);
            ParticleSpawner.instance.SpawnParticleEffect(hit.transform.position, Vector2.zero, ParticleSpawner.ParticleTypes.Blood);


        }
        yield return null;
    }
}
