using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UnitStats
{

    public List<Statuses> Status = new List<Statuses>();
    public bool CanMove
    {
        get
        {
            return !Status.Any(x => _disables.Contains(x));
        }
    }
    private Statuses[] _disables = new Statuses[] { Statuses.Frozen, Statuses.Stunned };

    public int Strength = 1;
    public int Intelligence = 1;
    public int Agility = 1;

    public delegate void LevelUp();
    public event LevelUp OnLevelUp;

    public int Experience = 0;
    public int Level = 1;

    private int _statsPerLevel = 4;
    private int _experiencePerLevel = 200;
    private int _experienceRequirementIncreasePerLevel = 50;
    public void GainExperience(int xp)
    {
        if (xp >= _experiencePerLevel)
        {
            if (OnLevelUp != null)
                OnLevelUp();

            Level++;
            Strength += _statsPerLevel;
            Intelligence += _statsPerLevel;
            Agility += _statsPerLevel;

            xp -= _experiencePerLevel;
            _experiencePerLevel += _experienceRequirementIncreasePerLevel;
            GainExperience(xp);
        }
        Experience += xp;
    }

    public void SetStatus(MonoBehaviour any, float duration = 0, params Statuses[] stati)
    {
        Status.AddRange(stati);
        if (duration != 0)
        {
            any.StartCoroutine(DelayedRemoveStatus(duration, stati));
        }
    }

    IEnumerator DelayedRemoveStatus(float duration, Statuses[] stati)
    {
        yield return new WaitForSeconds(duration);
        RemoveStatus(stati);
    }

    public void RemoveStatus(params Statuses[] stati)
    {
        foreach (var status in stati)
        {
            Status.Remove(status);
        }
    }

    internal bool HasStatus(Statuses slowed)
    {
        return Status.Contains(slowed);
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
    Slowed,
}