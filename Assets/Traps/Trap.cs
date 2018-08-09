using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(CircleCollider2D))]
public class Trap : MonoBehaviour
{

    Animator anim;
    CircleCollider2D col;

    [SerializeField] private float _armTime;
    [SerializeField] private float _resetTime;
    [SerializeField] private float _damage = 25;

    private bool _attacking = false;

    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer(LayerConstants.IgnoreProjectiles);
        anim = GetComponent<Animator>();
        col = GetComponent<CircleCollider2D>();
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!_attacking && collider.tag == Tags.Player)
        {
            StartCoroutine(Arm());
            _attacking = true;
        }
    }

    IEnumerator Arm()
    {
        anim.SetTrigger("Arm");
        yield return new WaitForSeconds(_armTime);
        yield return Attack();
    }

    IEnumerator Attack()
    {
        anim.SetTrigger("Attack");
        var hits = Physics2D.OverlapCircleAll(transform.position, col.radius, LayerMask.GetMask("Players", "Enemies"));
        foreach (var hit in hits)
        {
            var unit = hit.GetComponent<Unit>();
            if (unit != null)
            {
                unit.TakeDamage(_damage, gameObject, col);
                ParticleSpawner.instance.SpawnParticleEffect(transform.position, ParticleTypes.BloodParticles, lifetime: 1);
            }
        }
        yield return new WaitForSeconds(_resetTime);
        yield return Reset();
    }

    IEnumerator Reset()
    {
        anim.SetTrigger("Reset");
        _attacking = false;
        yield return null;
    }
}
