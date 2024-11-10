using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableObjects/EnemyStats", order = 1)]
public class EnemyStats : ScriptableObject
{
    public EnemyTypeEnum type;
    public int maxHealth;
    public float moveSpeed;
    public float attackCooldown;
}
