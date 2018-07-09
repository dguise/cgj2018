using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    private float _health { get; set; }
    public float Health {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            if (_health < 0)
            {
                _health = 0;
            }
        }
    }
    public bool IsDead
    {
        get
        {
            return Health <= 0;
        }
    }
    
    public float TakeDamage(float damage)
    {
        Health -= damage;
        return Health;
    }
}
