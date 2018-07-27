using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerupManager : MonoBehaviour {
    public static PowerupManager instance = null;

    Dictionary<string, GameObject> powerups = new Dictionary<string, GameObject>();
    string[] keys;

    void Awake () {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        var objs = PrefabRepository.instance.AllPowerups;
        foreach (var obj in objs)
            powerups.Add(obj.name, obj);

        keys = powerups.Keys.ToArray();

    }

    public void SpawnPowerup(string powerupName, Vector3 pos)
    {
        var powerup = powerups[powerupName];
        if (powerup != null)
            Instantiate(powerup, pos, Quaternion.identity);
    }

    public void SpawnRandomPowerUp(Vector3 pos)
    {
        var randomKey = keys[Random.Range(0, keys.Length)];
        SpawnPowerup(randomKey, pos);
    }


}
