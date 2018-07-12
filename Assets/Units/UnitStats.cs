using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class UnitStats
{
    public List<Statuses> Status = new List<Statuses>();
    public bool CanMove {
        get
        {
            return Status.Any(x => _disables.Contains(x));
        }
    }
    private Statuses[] _disables = new Statuses[] { Statuses.Frozen, Statuses.Stunned };

    public int Strength = 1;
    public int Intelligence = 1;
    public int Agility = 1;

    public Action OnLevelUp;
    public int Experience = 0;
    public int Level = 1;
    private int _experiencePerLevel = 200;
    public void GainExperience(int xp)
    {
        xp += Experience;
        if (xp >= _experiencePerLevel)
        {
            // TODO: Graphical level up?
            if (OnLevelUp != null)
                OnLevelUp();
            Level++;
            xp -= _experiencePerLevel;
            _experiencePerLevel += 50;
            GainExperience(xp);
        }
        Experience += xp;
        Debug.Log("You are now level " + Level + " with exp: " + Experience);
    }
}

public enum Statuses {
    Invisible,
    Invincible,
    Sneaking,
    Burning,
    Cold,
    Frozen,
    Stunned,
    Bleeding,
}