using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public float invincibilityDuration = 1.5f; 
    private bool isInvincible = false;  
    private SpriteRenderer spriteRenderer;
    private PlayerMovement playerMovement;
    [SerializeField] private float flashInterval = 0.1f;
    public CameraShakeManager cameraShakeManager;
    CinemachineImpulseSource impulseSource;
    public bool isDead = false;
    private Animator anim;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
        if (spriteRenderer == null) Debug.LogError("(PlayerHealth) No SpriteRenderer found on Player");
        impulseSource = GetComponent<CinemachineImpulseSource>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        currentHealth = GlobalManager.Instance.playerData.playerMaxHealth;

    }

    public void GetHit(int damage)
    {
        if (isInvincible || playerMovement.isDashing || playerMovement.isInGracePeriod) return;

        currentHealth -= damage;
        cameraShakeManager.CameraShake(impulseSource);
        Debug.Log($"(PlayerHealth) Player now has {currentHealth} HP.");

        if (currentHealth <= 0)
        {
            anim.SetTrigger("Death");
            isInvincible = true;
            spriteRenderer.sortingOrder = 3;
            Debug.Log("(PlayerHealth) Player dies");
        }
        else
        {
            StartCoroutine(InvincibilityCoroutine());
        }
    }

    public void IsDead()
    {
        isDead = true;
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
