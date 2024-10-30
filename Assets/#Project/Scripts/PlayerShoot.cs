using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;
    private InputAction shoot;
    [SerializeField] float shootingDelay = 0.5f;
    private float lastShotTime;

    private void Awake()
    {
        shoot = inputActions.FindActionMap("Player").FindAction("Shoot");
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
        bool shootingInput = shoot.ReadValue<float>() == 1;
        if (shootingInput && Time.time >= lastShotTime + shootingDelay)
        {
            Shoot();
            lastShotTime = Time.time;
        }
    }

    private void Shoot()
    {
        GameObject bullet = BulletPool.SharedInstance.GetPooledObject(); 
        if (bullet != null) 
        {
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
        }
    }
}
