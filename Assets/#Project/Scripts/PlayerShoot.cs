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
    private float lastShotTime;
    private Vector2 facingDirection;
    private PlayerMovement playerMovement;
    private Vector2 LastDirection
    {
        get { return playerMovement.lastDirection; } 
        set { playerMovement.lastDirection = value; } 
    }

    [SerializeField, Space (20f)] bool debug = false;

    private void Awake()
    {
        shoot = inputActions.FindActionMap("Player").FindAction("Shoot");
        shootDirection = inputActions.FindActionMap("Player").FindAction("Shoot Direction");
        playerMovement = GetComponent<PlayerMovement>();
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
        facingDirection = new Vector2(
            (LastDirection.x > 0)? 1 : ((LastDirection.x < 0)? -1 : 0),
            (LastDirection.y > 0)? 1 : ((LastDirection.y < 0)? -1 : 0)
        );       
    
        Vector2 shootAmount = (shootDirection.ReadValue<Vector2>() != Vector2.zero)? shootDirection.ReadValue<Vector2>().normalized: facingDirection.normalized;

        bool shootingInput = shoot.ReadValue<float>() == 1;
        if (shootingInput && Time.time >= lastShotTime + shootingDelay)
        {
            Shoot(shootAmount);
            lastShotTime = Time.time;
        }

        if (debug) 
        {
            Debug.Log($"[PlayerShoot] Shoot direction input = {shootDirection.ReadValue<Vector2>()}");
            Debug.Log($"[PlayerShoot] Facing direction = {facingDirection}");
        }

    }

    //Next step: have the last direction memorized or a default direction
    //so that when there's no direction pressed the bullet still has a direction

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
}
