using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;

    private void Start()
    {
        currentHealth = GlobalManager.Instance.playerData.playerMaxHealth;
    }

    public void GetHit(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"(PlayerHealth) Player now has {currentHealth} HP.");

        if (currentHealth <= 0)
        {
            Debug.Log("(PlayerHealth) Player dies");
        }
    }


}
