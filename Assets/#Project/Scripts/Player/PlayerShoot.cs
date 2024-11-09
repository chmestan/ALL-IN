using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] Camera mainCamera;

    [Header("Input Map")]
    [SerializeField] InputActionAsset inputActions;
    private InputAction shootInput;
    private InputAction shootDirectionInput;


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
        playerMovement = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();

        if (mainCamera == null)
            mainCamera = Camera.main;

        // Subscribe to device change events
        InputSystem.onDeviceChange += OnDeviceChange;
        
        DetectCurrentInputMode();
        SwitchInputMap();
    }

    private void OnEnable()
    {
        EnableInputMap();
    }

    private void OnDisable()
    {
        DisableInputMap();
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (change == InputDeviceChange.Added || change == InputDeviceChange.Removed)
        {
            DetectCurrentInputMode();
            SwitchInputMap();
        }
    }

    private void DetectCurrentInputMode()
    {
        useGamepad = (Gamepad.current != null); 
    }

    private void SwitchInputMap()
    {
        if (useGamepad)
        {
            inputActions.FindActionMap("PlayerKeyboard").Disable();
            inputActions.FindActionMap("PlayerGamepad").Enable();
            shootInput = inputActions.FindActionMap("PlayerGamepad").FindAction("Shoot");
            shootDirectionInput = inputActions.FindActionMap("PlayerGamepad").FindAction("Shoot Direction");
        }
        else
        {
            inputActions.FindActionMap("PlayerGamepad").Disable();
            inputActions.FindActionMap("PlayerKeyboard").Enable();
            shootInput = inputActions.FindActionMap("PlayerKeyboard").FindAction("Shoot");
            shootDirectionInput = inputActions.FindActionMap("PlayerKeyboard").FindAction("Shoot Direction");
        }
    }

    private void EnableInputMap()
    {
        if (useGamepad)
            inputActions.FindActionMap("PlayerGamepad").Enable();
        else
            inputActions.FindActionMap("PlayerKeyboard").Enable();
    }

    private void DisableInputMap()
    {
        inputActions.FindActionMap("PlayerKeyboard").Disable();
        inputActions.FindActionMap("PlayerGamepad").Disable();
    }

    private void Update()
    {
        ShootingDirection();
        AnimPlayer();
        if (debug) DebugLogs();
    }

    private void ShootingDirection()
    {
        if (!useGamepad)
        {
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            shootDirection = (mousePos - (Vector2)transform.position).normalized; // direction to mouse
        }
        else
        {
            shootDirection = shootDirectionInput.ReadValue<Vector2>().normalized;
            if (shootDirection != Vector2.zero)
            {
                LastDirection = shootDirection;
            }
        }

        bool shootingInput = (shootInput.ReadValue<float>() == 1);
        if (shootingInput && Time.time >= lastShotTime + shootingDelay)
        {
            Shoot(LastDirection);
            lastShotTime = Time.time;
        }
    }

    private Vector2 SnapToEightDirections(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float snappedAngle = Mathf.Round(angle / 45f) * 45f;
        return new Vector2(Mathf.Cos(snappedAngle * Mathf.Deg2Rad), Mathf.Sin(snappedAngle * Mathf.Deg2Rad)).normalized;
    }

    private void Shoot(Vector2 direction)
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
            Debug.Log($"[PlayerShoot] Input mode: {(useGamepad ? "Gamepad" : "Keyboard/Mouse")}");
        }
    }
}
