using UnityEngine;
using System.Collections;
using System;

public abstract class Ability
{
    public GameObject Owner { get; set; }
    public Player OwnerPlayer { get; set; }
    protected abstract float Cooldown { get; set; }

    protected float LastAttack = Single.MinValue;

    public Ability(GameObject go)
    {
        Owner = go;
        OwnerPlayer = go.GetComponent<Player>();
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
        OwnerPlayer.StartCoroutine(ActuallyUse());
    }

    public abstract IEnumerator ActuallyUse();
}
