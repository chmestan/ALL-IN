using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public abstract class Enemy : EnemyDefaultStateLogic
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

    public UnityEvent OnDeath = new UnityEvent();



    // private ChaseStateCollider chaseStateCollider;

// GET NAVMESH, STATS AND STATES
    private void Awake() 
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        GetScriptableObject();

        StateMachine = new EnemyStateMachine();
        SpawnState = new EnemySpawnState(this, StateMachine);
        RoamState = new EnemyRoamState(this, StateMachine);
        RetreatState = new EnemyRetreatState(this, StateMachine);
        ChaseState = new EnemyChaseState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);
    }

// INIT STATE AND CHASE COLLIDER
    private void Start()
    {
        // StateMachine.Initialize(SpawnState); 

        // chaseStateCollider = GetComponentInChildren<ChaseStateCollider>();
        // if (chaseStateCollider != null) chaseStateCollider.SetColliderRadius(stats.ChaseRadius); 
        // else Debug.LogError($"(Enemy) ChaseCollider not found on {gameObject.name}.");
    }

    private void OnEnable()
    {
        currentHealth = stats.MaxHealth;
        StateMachine.Initialize(SpawnState);
    }

    private void OnDisable()
    {
        agent.isStopped = true; 
        agent.ResetPath();  
    }

    private void Update()
    {
        StateMachine.Update();
    }




    #region GET HIT AND DIE METHODS
        public void GetHit(int damage)
        {
            if (StateMachine.CurrentEnemyState == SpawnState) return;
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
            OnDeath.Invoke(); 
            gameObject.SetActive(false);
        }
    #endregion

    #region INIT METHODS
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

        public virtual EnemyState GetStartingState()
        {
            return RoamState;

            // switch (stateEnum)
            // {
            //     case EnemyStateEnum.Spawn:
            //         return SpawnState;
            //     case EnemyStateEnum.Roam:
            //         return RoamState;
            //     case EnemyStateEnum.Retreat:
            //         return RetreatState;
            //     case EnemyStateEnum.Chase:
            //         return ChaseState;
            //     case EnemyStateEnum.Attack:
            //         return AttackState;
            //     default:
            //         Debug.LogError("Invalid state enum provided.");
            //         return RoamState; 
            // }
        }
    #endregion
    
    #region DEBUG
    //     private void OnDrawGizmos()
            // {
            //     Gizmos.color = Color.red;
            //     Gizmos.DrawWireSphere(transform.position, GetComponentInChildren<CircleCollider2D>().radius);
            // }
    #endregion
}

