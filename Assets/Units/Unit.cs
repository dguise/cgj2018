using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
public abstract class Unit : MonoBehaviour
{
    public Sprite Portrait;

    [HideInInspector]
    public Vector3 UnitBodyDirection;
    [HideInInspector]
    public Rigidbody2D RigidBody;

    [SerializeField]
    [Range(0, 10)]
    public float movementSpeed;
    public UnitStats Stats = new UnitStats();

    public int ExperienceWorth = 1;

    [SerializeField]
    [Range(10f, 2000f)]
    public float maxHealth;
    private float _health;
    public float Health
    {
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

    public delegate void DeathEvent(Unit unit);
    public event DeathEvent OnDeath;

    private void Awake()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        Health = maxHealth;
    }

    public float TakeDamage(float damage, GameObject sender, Collider2D collider)
    {
        if (Stats.Status.Contains(Statuses.Invincible))
            return 0f;

        Health -= damage;
        if (damage > 0)
        {
            TextManager.CreateDamageText(damage.ToString(), transform, 0.2f);
        }
        else if (damage < 0)
        {
            TextManager.CreateHealText((-1 * damage).ToString(), transform, 0.2f);
        }
        TakeDamageExtender(damage, sender, collider);

        if (IsDead)
        {
            if (OnDeath != null)
                OnDeath(this);

            if (gameObject.tag == Tags.Player)
            {
                Rigidbody2D rigid = gameObject.GetComponent<Rigidbody2D>();
                rigid.velocity = Vector2.zero;
                gameObject.transform.rotation = Quaternion.Euler(90, 0, 90);
                rigid.constraints = RigidbodyConstraints2D.FreezeAll;
                if (PlayerManager.PlayersAlive <= 0)
                {
                    GameObject manager = GameObject.Find("ManagerManager");
                    CustomGameManager gm = manager.GetComponent<CustomGameManager>();
                    gm.GameOver();
                }
            }
            else
            {
                if (sender != null)
                {
                    var killer = sender.GetComponent<Unit>();
                    if (killer != null)
                        killer.Stats.GainExperience(ExperienceWorth);
                }
                // TODO: Refactor this to event ffs
                var spawnyThing = GetComponent<SpawnThingOnDeath>();
                if (spawnyThing != null)
                    spawnyThing.SpawnThing();

                Destroy(gameObject);
                // Play generic death particle & sound?
                ParticleSpawner.instance.SpawnParticleEffect((Vector2)collider.transform.position, ParticleTypes.RedPixelExplosion_Up, (gameObject.transform.position - collider.transform.position).normalized);
            }

        }

        return Health;
    }

    public abstract void TakeDamageExtender(float damage, GameObject sender, Collider2D collision);
}
