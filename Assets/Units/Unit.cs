using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
public abstract class Unit : MonoBehaviour
{
    [SerializeField]
    [Range(0, 10)]
    protected float movementSpeed;
    [SerializeField]
    [Range(10f, 500f)]
    public float maxHealth;
    private float _health;
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

    private void Awake()
    {
        Health = maxHealth;
    }

    public float TakeDamage(float damage, GameObject sender, Collider2D collider)
    {
        Health -= damage;
        if (damage > 0)
        {
            TextManager.CreateDamageText(damage.ToString(), transform, 0.2f);
        } else {
            TextManager.CreateHealText((-1 * damage).ToString(), transform, 0.2f);
        }
        TakeDamageExtender(damage, sender, collider);

        if (IsDead) {
            if(gameObject.tag == Tags.Player) {
                PlayerManager.playerReady[gameObject.GetComponent<Player>().playerID] = false;
                PlayerManager.players -= 1;
            } else {
                Destroy(gameObject);
            }
        }

        return Health;
    }

    public abstract void TakeDamageExtender(float damage, GameObject sender, Collider2D collision);
}
