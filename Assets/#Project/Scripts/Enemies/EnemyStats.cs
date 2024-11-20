using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableObjects/EnemyStats", order = 1)]
public class EnemyStats : ScriptableObject
{
    [SerializeField] private int _MaxHealth = 10;
    [SerializeField] private float _MoveSpeed = 3f;
    [SerializeField] private float _AttackCooldown = 2f;

    [SerializeField] private EnemyStateEnum _StartingStateValue = EnemyStateEnum.Spawn;
    [SerializeField] private float _RoamingDistance = 3f;
    [SerializeField] private float _ChaseRadius = 2f;

    public int MaxHealth // read only atm
    {
        get => _MaxHealth;
        // set => maxHealth = value; 
    }

    public float MoveSpeed // read only atm
    {
        get => _MoveSpeed;
        // set => moveSpeed = value; 
    }

    public float AttackCooldown // read only atm
    {
        get => _AttackCooldown;
        // set => attackCooldown = value;
    }
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

}
