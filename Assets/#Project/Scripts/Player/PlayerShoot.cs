using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    private InputDeviceHandler inputMgr;
    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;
    private Animator anim;

    [Header("Shooting"), Space(10f)]
        [SerializeField] float shootingDelay = 0.5f;
        private Vector2 shootDirection;
        private float lastShotTime;
        private Vector2 LastDirection
        {
            get { return playerMovement.lastDirection.normalized; }
            set { playerMovement.lastDirection = value; }
        }

    [Header("Audio"), Space(3f)]
        private AudioManager audioManager;
        [SerializeField] List<AudioClip> shootAudioClips; 
        [SerializeField] private float shootVolModifierdB = 0f;

    [SerializeField, Space(20f)] bool debug = false;

    private void Awake()
    {
        inputMgr = GlobalManager.Instance.GetComponent<InputDeviceHandler>();
        playerMovement = GetComponent<PlayerMovement>();
        playerHealth = GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();

        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    private void Start()
    {
        shootingDelay = GlobalManager.Instance.playerData.playerShootingFrequency;
        audioManager = GlobalManager.Instance.GetComponentInChildren<AudioManager>();
    }

    private void Update()
    {
        // if (arenaMgr.state == ArenaStateEnum.Paused) return;
        if (playerHealth.currentHealth <= 0) return;
        Shoot(FacingDirection());
        AnimPlayer();
        if (debug) DebugLogs();
    }

    private Vector2 FacingDirection()
    {
        if (!inputMgr.useGamepad)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            shootDirection = (mousePos - (Vector2)transform.position); // direction to mouse
            LastDirection = shootDirection.normalized;
        }
        else
        {
            shootDirection = inputMgr.shootDirectionInput.ReadValue<Vector2>();
            if (shootDirection.magnitude > 0)
            {
                LastDirection = shootDirection.normalized;
            }
        }
        return LastDirection;
    }
    
    private void Shoot(Vector2 facingDir)
    {
        bool shootingInput = (inputMgr.shootInput.ReadValue<float>() == 1);
        if (shootingInput && Time.time >= lastShotTime + shootingDelay)
        {
            BulletShot(facingDir);
            PlayRandomShootSound();
            lastShotTime = Time.time;
        }

    }

    // private Vector2 SnapToEightDirections(Vector2 direction)
    // {
    //     float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //     float snappedAngle = Mathf.Round(angle / 45f) * 45f;
    //     return new Vector2(Mathf.Cos(snappedAngle * Mathf.Deg2Rad), Mathf.Sin(snappedAngle * Mathf.Deg2Rad)).normalized;
    // }

    private void BulletShot(Vector2 direction)
    {
        GameObject bullet = PlayerBulletsPool.SharedInstance.GetPooledObject();
        if (bullet != null)
        {
            BulletMovement bulletMvmt = bullet.GetComponent<BulletMovement>();
            bullet.transform.position = transform.position;
            bullet.SetActive(true);
            bulletMvmt.SetDirection(direction);
        }
        if (debug) Debug.Log($"[BulletShot] Direction: {direction}, Magnitude: {direction.magnitude}");

    }

    private void PlayRandomShootSound()
    {
        if (shootAudioClips != null && shootAudioClips.Count > 0)
        {
            int randomIndex = Random.Range(0, shootAudioClips.Count); 
            AudioClip randomClip = shootAudioClips[randomIndex]; 

            audioManager.PlaySFX(randomClip, shootVolModifierdB);
        }
    }


    private void AnimPlayer()
    {
        anim.SetFloat("LastMoveX", LastDirection.x);
        anim.SetFloat("LastMoveY", LastDirection.y);
    }

    private void DebugLogs()
    {
        if (debug)
        {
            // Debug.Log($"[PlayerShoot] Input mode: {(useGamepad ? "Gamepad" : "Keyboard/Mouse")}");
        }
    }
}
