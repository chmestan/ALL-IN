using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour, IEnemy
{
    [Header("Enemy Stats")]
        [SerializeField] private EnemyStats stats;
        protected int currentHealth;

    protected NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void OnEnable()
    {
        InitializeEnemy();
    }
    private void OnDisable()
    {
    }

    private void InitializeEnemy()
    {
        currentHealth = stats.maxHealth;
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

    public void Attack()
    {

    }
}

