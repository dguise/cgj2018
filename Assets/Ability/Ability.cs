using UnityEngine;
using System.Collections;
using System;

public abstract class Ability
{
    public GameObject Owner { get; set; }
    public Unit OwnerUnit { get; set; }
    protected abstract float Cooldown { get; set; }

    protected float LastAttack = Single.MinValue;

    public Ability(GameObject go)
    {
        Owner = go;
        OwnerUnit = go.GetComponent<Unit>();
    }

    public bool CanUse {
        get
        {
            return Time.time > (LastAttack + Cooldown);
        }
    }

    public void Use()
    {
        LastAttack = Time.time;
        OwnerUnit.StartCoroutine(ActuallyUse());
    }

    public abstract IEnumerator ActuallyUse();
}
