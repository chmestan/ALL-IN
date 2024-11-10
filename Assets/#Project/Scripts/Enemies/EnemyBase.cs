using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IEnemy
{
    [Header("Enemy Stats")]
        [SerializeField] private EnemyStats stats;
        protected int currentHealth;


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
        Debug.Log($"{stats.type} took {damage} damage! Current Health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log($"{stats.type} died.");
        gameObject.SetActive(false);
    }

    public void Attack()
    {

    }
}

