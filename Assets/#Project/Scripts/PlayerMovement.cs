using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Movement")]
    [SerializeField] InputActionAsset inputActions;
    [SerializeField] float speed = 9f;
    [SerializeField] float acceleration = 30f;
    [SerializeField] float deceleration = 80f;
    [SerializeField] float turnSpeed = 1f;
    [SerializeField] float turnSpeedCompensation = 9f;
    private Vector2 currentVelocity = Vector2.zero;    
    private InputAction move;

    private void Awake()
    {
        move = inputActions.FindActionMap("Player").FindAction("move");
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
        Vector2 moveAmount = move.ReadValue<Vector2>();
        Vector2 targetVelocity = moveAmount.normalized * speed;

        bool isChangingDirection = Vector2.Dot(currentVelocity, targetVelocity) < 0;
        if (isChangingDirection) turnSpeed = turnSpeedCompensation;
        else turnSpeed = 1f;

        currentVelocity = Vector2.MoveTowards(currentVelocity, targetVelocity,
            (moveAmount.magnitude > 0 ? acceleration : deceleration) * Time.deltaTime * turnSpeed);

        transform.Translate(currentVelocity * Time.deltaTime);
    }



}
