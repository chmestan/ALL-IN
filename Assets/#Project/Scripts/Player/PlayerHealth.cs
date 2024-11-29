using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public float invincibilityDuration = 1.5f; 
    private bool isInvincible = false;  
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float flashInterval = 0.1f;

    private void Start()
    {
        currentHealth = GlobalManager.Instance.playerData.playerMaxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) Debug.LogError("(PlayerHealth) No SpriteRenderer found on Player");

    }

    public void GetHit(int damage)
    {
        if (isInvincible) return;

        currentHealth -= damage;
        Debug.Log($"(PlayerHealth) Player now has {currentHealth} HP.");

        if (currentHealth <= 0)
        {
            Debug.Log("(PlayerHealth) Player dies");
        }
        else
        {
            StartCoroutine(InvincibilityCoroutine());
        }
    }


    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        Debug.Log("(PlayerHealth) Player is invincible");

        yield return StartCoroutine(FlashWhileInvincible());

        isInvincible = false;
        Debug.Log("(PlayerHealth) Player is no longer invincible.");
    }

    private IEnumerator FlashWhileInvincible()
    {
        for (float t = 0; t < invincibilityDuration; t += flashInterval)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled; 
            yield return new WaitForSeconds(flashInterval);
        }
        spriteRenderer.enabled = true; 
    }

}
