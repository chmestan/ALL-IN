using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [Header ("Input Map")]
    [SerializeField] InputActionAsset inputActions;

    [Header ("Shooting"), Space (10f)]
    [SerializeField] float shootingDelay = 0.5f;

    
    private InputAction shoot;
    private InputAction shootDirection;
    private Vector2 shootAmount;
    private float lastShotTime;
    private PlayerMovement playerMovement;
    private Vector2 LastDirection
    {
        get { return playerMovement.lastDirection; } 
        set { playerMovement.lastDirection = value; } 
    }

    private Animator anim;

    [SerializeField, Space (20f)] bool debug = false;

    private void Awake()
    {
        shoot = inputActions.FindActionMap("Player").FindAction("Shoot");
        shootDirection = inputActions.FindActionMap("Player").FindAction("Shoot Direction");
        playerMovement = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();
    }

    private void Update()
    {
        UpdateShoot();
        AnimPlayer();
        if (debug) DebugLogs();
    }

    private void UpdateShoot()
    {
        if (shootDirection.ReadValue<Vector2>() != Vector2.zero) 
        {
            LastDirection = shootDirection.ReadValue<Vector2>();
        }
        shootAmount = LastDirection.normalized;

        bool shootingInput = (shoot.ReadValue<float>() == 1);
        if (shootingInput && Time.time >= lastShotTime + shootingDelay)
        {
            Shoot(shootAmount);
            lastShotTime = Time.time;
        }

    }
    private void Shoot(Vector2 direction)
    {
        Vector2 bulletDirection = direction;
        GameObject bullet = BulletPool.SharedInstance.GetPooledObject(); 
        if (bullet != null) 
        {
            BulletMovement bulletMvmt = bullet.GetComponent<BulletMovement>();
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
            bulletMvmt.SetDirection(bulletDirection);
        }
    }

    private void AnimPlayer()
    {
        anim.SetFloat("LastMoveX", shootAmount.x);
        anim.SetFloat("LastMoveY", shootAmount.y);
    }

    private void DebugLogs()
    {
        if (debug) 
        {
            Debug.Log($"[PlayerShoot] Shoot direction input = {shootDirection.ReadValue<Vector2>()}");
            Debug.Log($"[PlayerShoot] Facing direction = {LastDirection}");
        }
    }
}
