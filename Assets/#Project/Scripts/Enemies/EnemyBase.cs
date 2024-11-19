using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour, IEnemy
{
    [Header("Enemy Stats")]
        private EnemyStats stats;
        protected int currentHealth;
        protected EnemyStateEnum state;

    protected NavMeshAgent agent;

    private void Awake() 
    {
        GetScriptableObject();
    }

    private void Start()
    {
        // nav mesh
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        state = stats.StartingState;
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

    public void Update()
    {
        switch(state)
        {
            case EnemyStateEnum.Idle:
                Idle();
                break;
            case EnemyStateEnum.Shoot:
                Shoot();
                break;
            case EnemyStateEnum.Follow:
                Follow();
                break;
            case EnemyStateEnum.Retreat:
                Retreat();
                break;

        }
    }

    protected virtual void Idle()
    {
        
    }
    protected virtual void Shoot()
    {

    }
    protected virtual void Follow()
    {
        if (Player.Instance != null)
        {
            agent.speed = stats.MoveSpeed;
            agent.SetDestination(Player.Instance.transform.position);
        }
    }
    protected virtual void Retreat()
    {

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

