using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrefabRepository : MonoBehaviour
{
    public float LoadingProgressPercentage { get; internal set; }
    private readonly int _loadingCategories = 5;
    private int _internalProgress = 0;

    public static PrefabRepository instance = null;

    public GameObject TextManagerScreenCanvas { get; private set; }
    public APopupText DamageTextParent { get; private set; }
    public GameObject Player { get; private set; }
    public GameObject[] AllRegularRooms { get; private set; }
    public GameObject[] AllBossRooms { get; private set; }
    public GameObject[] AllPowerups { get; private set; }
    public GameObject EnemySpawnerBullet { get; private set; }
    public GameObject Bullet { get; private set; }
    public GameObject EnemyBullet { get; private set; }
    public GameObject ImmolationBullet { get; private set; }
    public GameObject MeleeBullet { get; private set; }
    public GameObject PlayerMeleeBullet { get; private set; }
    public GameObject ShieldBullet { get; private set; }
    public GameObject SpecialBullet { get; private set; }
    public readonly List<AudioClip[]> Songs = new List<AudioClip[]>();
    public readonly List<AudioClip> Sounds = new List<AudioClip>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);


        // Text manager
        TextManagerScreenCanvas = Resources.Load<GameObject>("ScreenCanvas");
        DamageTextParent = Resources.Load<APopupText>("DamageTextParent");
        IncrementProgress();

        // Room generation
        AllRegularRooms = Resources.LoadAll<GameObject>("Regular");
        AllBossRooms = Resources.LoadAll<GameObject>("Boss");
        IncrementProgress();

        // Bullets
        EnemyBullet = Resources.Load<GameObject>("EnemyBullet");
        EnemySpawnerBullet = Resources.Load<GameObject>("EnemySpawnerBullet");
        Bullet = Resources.Load<GameObject>("Bullet");
        ImmolationBullet = Resources.Load<GameObject>("ImmolationBullet");
        MeleeBullet = Resources.Load<GameObject>("MeleeBullet");
        PlayerMeleeBullet = Resources.Load<GameObject>("PlayerMeleeBullet");
        ShieldBullet = Resources.Load<GameObject>("ShieldBullet");
        SpecialBullet = Resources.Load<GameObject>("SpecialBullet");
        IncrementProgress();

        // Audio
        string[] songsToLoad = { "overworld", "spooky", "fight" };
        foreach (string song in songsToLoad)
        {
            Songs.Add(
                new AudioClip[] {
                    (Resources.Load(song + "-1") as AudioClip),
                    (Resources.Load(song + "-2") as AudioClip)
                    });
        }
        string[] soundsToLoad = {"crash1", "crash2", "crash3", "crash4",
                                "plopp1", "plopp2", "mouth1-1", "mouth2-1",
                                "mouth3-1", "mouth4-1", "mouth5-1", "tssss1",
                                "tssss2", "pew1", "shield", "swish1"};
        foreach (string sound in soundsToLoad)
        {
            Sounds.Add(Resources.Load(sound) as AudioClip);
        }
        IncrementProgress();

        // Misc
        Player = Resources.Load<GameObject>("Player3D");
        AllPowerups = Resources.LoadAll<GameObject>("powerups");
        IncrementProgress();
    }

    private void IncrementProgress()
    {
        LoadingProgressPercentage = ++_internalProgress / _loadingCategories;
    }
}
