using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableObjects/EnemyStats", order = 1)]
public class EnemyStats : ScriptableObject
{
    [Header ("HEALTH")]
    [SerializeField] private int _MaxHealth = 10;

    [Header ("SPEED"), Space (3f)]
    [SerializeField] private float _MoveSpeed = 3f;

    [Header ("STATES ATTRIBUTES"), Space (3f)]
    [SerializeField] private EnemyStateEnum _StartingStateValue = EnemyStateEnum.Spawn;
    [SerializeField] private float _RoamingDistance = 3f;
    [SerializeField] private float _ChaseRadius = 2f;

    [Header ("ATTACK STATS"), Space (3f)]
    [SerializeField] private int _BulletsPerBurst;
    [SerializeField] private float _TimeBetweenBullets;
    [SerializeField] private float _TimeBetweenBursts;
    [SerializeField] private GameObject bulletPrefab;

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
    public EnemyStateEnum StartingStateValue // read only atm
    {
        get => _StartingStateValue;
        // set => attackCooldown = value;
    }

    public float RoamingDistance
    {
        get => _RoamingDistance;
    }
    public float ChaseRadius
    {
        get => _ChaseRadius;
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

}
