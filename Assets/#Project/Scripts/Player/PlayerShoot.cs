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
    [SerializeField] Camera mainCamera;
    
    private InputAction shoot;
    private InputAction shootDirection;
    private Vector2 shootSnappedDir;
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

        if (mainCamera == null)
            mainCamera = Camera.main;
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
        ShootingDirection();
        AnimPlayer();
        if (debug) DebugLogs();
    }

    private void ShootingDirection()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 directionToMouse = (mousePos - (Vector2)transform.position).normalized;
        shootSnappedDir = SnapToEightDirections(directionToMouse);

        bool shootingInput = (shoot.ReadValue<float>() == 1);
        if (shootingInput && Time.time >= lastShotTime + shootingDelay)
        {
            Shoot(shootSnappedDir);
            lastShotTime = Time.time;
        }

        LastDirection = shootSnappedDir;
    }

    private Vector2 SnapToEightDirections(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Atan allows to find the angle between the positive X-axis and a(y,x) vector
        // It returns an angle in radians and [ * Mathf.Rad2Deg ] converts it to degrees
        float snappedAngle = Mathf.Round(angle / 45f) * 45f;
        // tells us which 45 degree angle we're closest to
        return new Vector2(Mathf.Cos(snappedAngle * Mathf.Deg2Rad), Mathf.Sin(snappedAngle * Mathf.Deg2Rad)).normalized;
        // converts our angle to a directional vector
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
        anim.SetFloat("LastMoveX", shootSnappedDir.x);
        anim.SetFloat("LastMoveY", shootSnappedDir.y);
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
