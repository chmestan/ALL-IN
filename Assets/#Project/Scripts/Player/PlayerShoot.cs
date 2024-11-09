using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    private InputDeviceHandler inputMgr;

    [Header("Input Map")]
    // [SerializeField] InputActionAsset inputActions;
    // private InputAction shootInput;
    // private InputAction shootDirectionInput;


    [Header("Shooting"), Space(10f)]
    [SerializeField] float shootingDelay = 0.5f;
    private Vector2 shootDirection;
    private float lastShotTime;
    private Vector2 LastDirection
    {
        get { return playerMovement.lastDirection; }
        set { playerMovement.lastDirection = value; }
    }


    private PlayerMovement playerMovement;
    private Animator anim;

    [Header("Gamepad?"), Space(10f)]
    [SerializeField] bool useGamepad = false;
    [SerializeField, Space(20f)] bool debug = false;

    private void Awake()
    {
        inputMgr = GlobalManager.Instance.GetComponent<InputDeviceHandler>();
        playerMovement = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();

        if (mainCamera == null)
            mainCamera = Camera.main;

    }

    private void Update()
    {
        Shoot(FacingDirection());
        AnimPlayer();
        if (debug) DebugLogs();
    }

    private Vector2 FacingDirection()
    {
        if (!inputMgr.useGamepad)
        {
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            shootDirection = (mousePos - (Vector2)transform.position).normalized; // direction to mouse
            LastDirection = shootDirection;
        }
        else
        {
            shootDirection = inputMgr.shootDirectionInput.ReadValue<Vector2>().normalized;
            if (debug) Debug.Log($"[PlayerShoot] Shoot input: {shootDirection}");
            if (shootDirection != Vector2.zero)
            {
                LastDirection = shootDirection;
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
        GameObject bullet = BulletPool.SharedInstance.GetPooledObject();
        if (bullet != null)
        {
            BulletMovement bulletMvmt = bullet.GetComponent<BulletMovement>();
            bullet.transform.position = transform.position;
            bullet.SetActive(true);
            bulletMvmt.SetDirection(direction);
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
