using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseStateCollider : MonoBehaviour
{
    private Enemy parentEnemy;

    private void Awake()
    {
        parentEnemy = GetComponentInParent<Enemy>();
        if (parentEnemy == null) Debug.LogError($"(ChaseCollider) Parent Enemy script not found on {gameObject.name}.");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            Debug.Log($"Player entered chase collider of {parentEnemy.name}.");
            parentEnemy.OnPlayerDetected();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            Debug.Log($"Player exited chase collider of {parentEnemy.name}.");
            parentEnemy.OnPlayerLost();
        }
    }


}
