using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnThingOnDeath : MonoBehaviour {
    public GameObject thing;

    public Unit unit { get; private set; }

    private void Start()
    {
        this.unit = GetComponent<Unit>();
    }

    internal void SpawnThing()
    {
        Instantiate(thing, transform.position, Quaternion.identity);
    }
}
