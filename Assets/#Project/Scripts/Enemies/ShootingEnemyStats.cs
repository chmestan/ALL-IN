using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableObjects/EnemyStats/ShootingEnemy", order = 2)]

public class ShootingEnemyStats : EnemyStats
{
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
