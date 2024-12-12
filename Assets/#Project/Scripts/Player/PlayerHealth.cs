using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    private UIDisplayManager hpDisplayManager;
    [SerializeField] private float invincibilityDuration = 1.5f; 
    [SerializeField] private float flashInterval = 0.1f;
    private bool isInvincible = false;  
    public bool isDead = false;

    #region References
        private SpriteRenderer spriteRenderer;
        private PlayerMovement playerMovement;
        private Animator anim;
        [SerializeField] private ParticleSystem bloodParticles; 
    #endregion

    #region Camera Shake
        public CameraShakeManager cameraShakeManager;
        CinemachineImpulseSource impulseSource;
    #endregion 

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
        bloodParticles = GetComponentInChildren<ParticleSystem>();
        if (spriteRenderer == null) Debug.LogError("(PlayerHealth) No SpriteRenderer found on Player");
        impulseSource = GetComponent<CinemachineImpulseSource>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        currentHealth = GlobalManager.Instance.playerData.playerMaxHealth;
        hpDisplayManager = FindObjectOfType<UIDisplayManager>();
        hpDisplayManager.Initialize(currentHealth);
    }

    public void GetHit(int damage)
    {
        if (isInvincible || playerMovement.isDashing || playerMovement.isInGracePeriod) return;

        currentHealth = Mathf.Max(0, currentHealth - damage);         
        hpDisplayManager.UpdateHPDisplay(currentHealth);

        if (currentHealth <= 0)
        {
            anim.SetTrigger("Death");
            DisableEnemyAgents();
            isInvincible = true;
            spriteRenderer.sortingOrder = 3;
        }
        else
        {
            StartCoroutine(InvincibilityCoroutine());
        }
    }
    private void DisableEnemyAgents()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            enemy.Agent.isStopped = true; 
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
