using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IEnemy
{
    private EnemyStats stats;
    public EnemyStats Stats
    { 
        get => stats;
    }
    protected int currentHealth;

    protected NavMeshAgent agent;
    public NavMeshAgent Agent
    { 
        get => agent;
    }

    #region State Machine
    public EnemyStateMachine StateMachine {get; set;}
    public EnemyIdleState IdleState {get; set;}
    public EnemyRetreatState RetreatState {get; set;}
    public EnemyFollowState FollowState {get; set;}
    public EnemyShootState ShootState {get; set;}
    #endregion

    private void Awake() 
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        GetScriptableObject();

        StateMachine = new EnemyStateMachine();
        IdleState = new EnemyIdleState(this, StateMachine);
        RetreatState = new EnemyRetreatState(this, StateMachine);
        FollowState = new EnemyFollowState(this, StateMachine);
        ShootState = new EnemyShootState(this, StateMachine);

    }

    private void Start()
    {
        EnemyState startingState = GetStartingState(stats.StartingStateValue);
        StateMachine.Initialize(startingState); // to be potentially overridden 
    }

    private void OnEnable()
    {
        InitializeEnemy();
    }

    private void InitializeEnemy()
    {
        currentHealth = stats.MaxHealth;
    }

    private void Update()
    {
        StateMachine.Update();
    }


    public void GetHit(int damage)
    {
        currentHealth -= damage;
        // Debug.Log($"(Enemy) {gameObject.name} took {damage} damage! Current Health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log($"{gameObject.name} died.");
        gameObject.SetActive(false);
    }

    private void GetScriptableObject()
    {
        string enemyName = gameObject.name.Replace("(Clone)", "").Trim();
        stats = Resources.Load<EnemyStats>($"ScriptableObjects/Enemy Types Stats/{enemyName}Stats");
        
        // Debug.Log($"(Enemy) Looking for EnemyStats: {enemyName}");

        if (stats == null)
        {
            Debug.LogError($"(Enemy) No EnemyStats found for {enemyName}. Ensure it exists in Resources/EnemyStats.");
        }
    }


    private EnemyState GetStartingState(EnemyStateEnum stateEnum)
    {
        switch (stateEnum)
        {
            case EnemyStateEnum.Idle:
                return IdleState;
            case EnemyStateEnum.Retreat:
                return RetreatState;
            case EnemyStateEnum.Follow:
                return FollowState;
            case EnemyStateEnum.Shoot:
                return ShootState;
            default:
                Debug.LogError("Invalid state enum provided.");
                return IdleState; 
        }
    }
}

