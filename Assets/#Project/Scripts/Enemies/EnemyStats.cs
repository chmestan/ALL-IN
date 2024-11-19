using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableObjects/EnemyStats", order = 1)]
public class EnemyStats : ScriptableObject
{
    [SerializeField] private int maxHealth;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackCooldown;

    [SerializeField] private EnemyStateEnum startingStateValue;
    [SerializeField] private float retreatDistance; // = too close
    [SerializeField] private float followDistance; // = too far

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



}
