using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class UnitStats
{
    public List<Statuses> Status = new List<Statuses>();
    public bool CanMove
    {
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
        if (xp >= _experiencePerLevel)
        {
            // TODO: Graphical level up?
            if (OnLevelUp != null)
                OnLevelUp();

            Level++;
            Strength += 4;
            Intelligence += 4;
            Agility += 4;

            xp -= _experiencePerLevel;
            _experiencePerLevel += 50;
            GainExperience(xp);
        }
        Experience += xp;
    }

    public void SetStatus(params Statuses[] stati)
    {
        Status.AddRange(stati);
    }

    public void RemoveStatus(params Statuses[] stati)
    {
        foreach (var status in stati)
        {
            Status.Remove(status);
        }
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