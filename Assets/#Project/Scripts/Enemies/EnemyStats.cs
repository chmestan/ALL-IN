using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableObjects/EnemyStats", order = 1)]
public abstract class EnemyStats : ScriptableObject
{
    [Header ("HEALTH")]
    [SerializeField] private int _MaxHealth = 10;

    [Header ("SPEED"), Space (3f)]
    [SerializeField] private float _MoveSpeed = 3f;

    [Header ("STATES ATTRIBUTES"), Space (3f)]
    [SerializeField] private float _RoamingDistance = 3f;


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


}
