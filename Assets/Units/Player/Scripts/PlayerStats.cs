using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class PlayerStats
{
    public static List<Statuses> Status = new List<Statuses>();
    public static bool CanMove {
        get
        {
            return Status.Any(x => _disables.Contains(x));
        }
    }
    private static Statuses[] _disables = new Statuses[] { Statuses.Frozen, Statuses.Stunned };

    public static int Strength = 1;
    public static int Intelligence = 1;
    public static int Agility = 1;
}

public enum Statuses {
    Invisible,
    Sneaking,
    Burning,
    Cold,
    Frozen,
    Stunned,
    Bleeding,
}