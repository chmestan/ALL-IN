using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableObjects/EnemyStats", order = 1)]
public class EnemyStats : ScriptableObject
{
    [Header ("HEALTH")]
    [SerializeField] private int _MaxHealth = 10;

    [Header ("SPEED"), Space (3f)]
    [SerializeField] private float _MoveSpeed = 3f;

    [Header ("STATES ATTRIBUTES"), Space (3f)]
    [SerializeField] private float _RoamingDistance = 3f;

    [Header ("DAMAGE STATS"), Space (3f)]
    [SerializeField] private int _BulletDamage = 5;
    [SerializeField] private int _CollisionDamage = 10;

    [Header ("ATTACK STATS"), Space (3f)]
    [SerializeField] private int _BulletsPerBurst = 0;
    [SerializeField] private float _TimeBetweenBullets = 0f;
    [SerializeField] private float _TimeBetweenBursts = 0f;
    [SerializeField] private GameObject bulletPrefab;

    [Header ("ATTACK <=> ROAM "), Space (3f)]
    [SerializeField] private int _MinBursts = 1; 
    [SerializeField] private int _MaxBursts = 3; // max included
    [SerializeField] private int _MinRoams = 1;
    [SerializeField] private int _MaxRoams = 3; // max included

    #region HEALTH
    public int MaxHealth // read only atm
    {
        get => _MaxHealth;
        // set => maxHealth = value; 
    }
    #endregion
    
    #region SPEED
    public float MoveSpeed // read only atm
    {
        get => _MoveSpeed;
        // set => moveSpeed = value; 
    }
    #endregion

    #region STATES ATTRIBUTES

    public float RoamingDistance
    {
        get => _RoamingDistance;
    }
    #endregion

    #region DAMAGE
    public int BulletDamage
    {
        get => _BulletDamage;
    }
    public int CollisionDamage
    {
        get => _CollisionDamage;
    }

    #endregion

    #region ATTACK STATS
    public float BulletsPerBurst
    {
        get => _BulletsPerBurst;
    }
    public float TimeBetweenBullets
    {
        get => _TimeBetweenBullets;
    }
    public float TimeBetweenBursts
    {
        get => _TimeBetweenBursts;
    }
    #endregion

    #region ATTACK <=> ROAM
    public int MinBursts
    {
        get => _MinBursts;
    }
    public int MaxBursts
    {
        get => _MaxBursts;
    }
    public int MinRoams
    {
        get => _MinRoams;
    }
    public int MaxRoams
    {
        get => _MaxRoams;
    }
    #endregion


}
