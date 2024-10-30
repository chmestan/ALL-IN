using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;
    private InputAction shoot;
    private InputAction shootDirection;
    [SerializeField] float shootingDelay = 0.5f;
    private float lastShotTime;

    private void Awake()
    {
        shoot = inputActions.FindActionMap("Player").FindAction("Shoot");
        shootDirection = inputActions.FindActionMap("Player").FindAction("Shoot Direction");
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

        Vector2 shootAmount = shootDirection.ReadValue<Vector2>().normalized;

        bool shootingInput = shoot.ReadValue<float>() == 1;
        if (shootingInput && Time.time >= lastShotTime + shootingDelay)
        {
            Shoot(shootAmount);
            lastShotTime = Time.time;
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
