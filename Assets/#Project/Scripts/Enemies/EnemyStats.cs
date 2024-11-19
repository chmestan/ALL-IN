using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableObjects/EnemyStats", order = 1)]
public class EnemyStats : ScriptableObject
{
    [SerializeField] private int maxHealth;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackCooldown = 2f;

    [SerializeField] private EnemyStateEnum startingStateValue;
    [SerializeField] private float _RoamingDistance = 3f;

    public int MaxHealth // read only atm
    {
        get => maxHealth;
        // set => maxHealth = value; 
    }

    public float MoveSpeed // read only atm
    {
        get => moveSpeed;
        // set => moveSpeed = value; 
    }

    public float AttackCooldown // read only atm
    {
        get => attackCooldown;
        // set => attackCooldown = value;
    }
    public EnemyStateEnum StartingStateValue // read only atm
    {
        get => startingStateValue;
        // set => attackCooldown = value;
    }

    public float RoamingDistance
    {
        get => _RoamingDistance;
    }

}
