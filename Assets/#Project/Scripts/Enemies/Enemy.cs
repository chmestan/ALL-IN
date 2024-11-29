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


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("player")) 
        {
            PlayerHealth playerHealth = Player.Instance.GetComponent<PlayerHealth>(); 
            if (playerHealth != null)
            {
                playerHealth.GetHit(stats.CollisionDamage);
            }
        }
    }

    // private IEnumerator ApplyCollisionDamage(GameObject other)
    // {
    //     PlayerHealth playerHealth = Player.Instance.GetComponent<PlayerHealth>(); 

    //     while (playerHealth != null && StateMachine.CurrentEnemyState != SpawnState)
    //     {
    //         playerHealth.GetHit(stats.CollisionDamage);
    //         Debug.Log($"(Enemy) Enemy inflicting collision damage for {stats.CollisionDamage} HP");
    //         yield return new WaitForSeconds(stats.CollisionDamageTick); 
    //     }
    // }

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

