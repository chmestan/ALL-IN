using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerShoot : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;
    private InputAction shoot;

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
        Debug.Log(shoot.ReadValue<float>());
        bool shooting = (shoot.ReadValue<float>()==1)? true: false;

        if (shooting)
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

}
