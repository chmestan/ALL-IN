using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableObjects/EnemyStats", order = 1)]
public class EnemyStats : ScriptableObject
{
    public string enemyName;    
    public int maxHealth;
    public float moveSpeed;
    public float attackCooldown;
}
