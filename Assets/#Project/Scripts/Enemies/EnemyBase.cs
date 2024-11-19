using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IEnemy
{
    [Header("Enemy Stats")]
        private EnemyStats stats;
        protected int currentHealth;
        protected EnemyStateEnum state;

    protected NavMeshAgent agent;

    #region State Machine
    public EnemyStateMachine StateMachine {get; set;}
    public EnemyIdleState IdleState {get; set;}
    public EnemyRetreatState RetreatState {get; set;}
    public EnemyFollowState FollowState {get; set;}
    public EnemyShootState ShootState {get; set;}
    #endregion

    private void Awake() 
    {
        StateMachine = new EnemyStateMachine();
        IdleState = new EnemyIdleState(this, StateMachine);
        RetreatState = new EnemyRetreatState(this, StateMachine);
        FollowState = new EnemyFollowState(this, StateMachine);
        ShootState = new EnemyShootState(this, StateMachine);

        GetScriptableObject();
    }

    private void Start()
    {
        // nav mesh
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        StateMachine.Initialize(IdleState); // to be potentially overridden 

    }

    private void OnEnable()
    {
        InitializeEnemy();
    }

    private void InitializeEnemy()
    {
        currentHealth = stats.MaxHealth;
    }

    public void GetHit(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{stats.name} took {damage} damage! Current Health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log($"{stats.name} died.");
        gameObject.SetActive(false);
    }

    private void GetScriptableObject()
    {
        string enemyName = gameObject.name.Replace("(Clone)", "").Trim();
        stats = Resources.Load<EnemyStats>($"ScriptableObjects/Enemy Types Stats/{enemyName}Stats");
        
        Debug.Log($"Looking for EnemyStats: {enemyName}");

        if (stats == null)
        {
            Debug.LogError($"No EnemyStats found for {enemyName}. Ensure it exists in Resources/EnemyStats.");
        }
    }

}

