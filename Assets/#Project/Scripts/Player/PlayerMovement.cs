using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Input Map")]
        [SerializeField] InputActionAsset inputActions;
        private InputAction move;
        private Vector2 moveAmount;

    [Header ("Movement"), Space (10f)]
        [SerializeField] float speed = 9f;
        [SerializeField] float acceleration = 30f;
        [SerializeField] float deceleration = 80f;
        [SerializeField] float turnSpeed = 1f;
        [SerializeField] float turnSpeedCompensation = 9f;
        public Vector2 lastDirection = new Vector2(1,0);
        private Vector2 currentVelocity = Vector2.zero;    

    [Header ("Technical"), Space (10f)]
        [SerializeField] float diagonalBufferTime = 0.1f;
        private float lastDirectionUpdateTime;

    [SerializeField] private ArenaState manager;

    private Animator anim;



    [SerializeField, Space (20) ] bool debug = false;

    private void Awake()
    {
        move = inputActions.FindActionMap("Player").FindAction("move");
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
        if (manager.state == ArenaStateEnum.Paused) return;
        Move();
        UpdateLastDirection();
        AnimPlayer();
    }

    private void Move()
    {
        moveAmount = move.ReadValue<Vector2>();

        if (debug) Debug.Log($"[PlayerMovement] Move Amount = {moveAmount}");
        Vector2 targetVelocity = moveAmount.normalized * speed ;

        bool isChangingDirection = Vector2.Dot(currentVelocity, targetVelocity) < 0;
        if (isChangingDirection) turnSpeed = turnSpeedCompensation;
        else turnSpeed = 1f;

        currentVelocity = Vector2.MoveTowards(currentVelocity, targetVelocity,
            (moveAmount.magnitude > 0 ? acceleration : deceleration) * Time.deltaTime * turnSpeed);

        transform.Translate(currentVelocity * Time.deltaTime);
    }

    private void UpdateLastDirection()
    {
        if (moveAmount != Vector2.zero)
        {
            bool isDiagonal = Mathf.Abs(moveAmount.x) > 0 && Mathf.Abs(moveAmount.y) > 0;
            if (isDiagonal || Time.time - lastDirectionUpdateTime > diagonalBufferTime)
            {
                lastDirection = moveAmount;
                lastDirectionUpdateTime = Time.time;
                // anim.SetFloat("LastMoveX", lastDirection.x);
                // anim.SetFloat("LastMoveY", lastDirection.y);
            }
        }   
    }

    private void AnimPlayer()
    {
        anim.SetFloat("MoveX", moveAmount.x);
        anim.SetFloat("MoveY", moveAmount.y);

        float magnitudeX = Math.Abs(moveAmount.x);
        float magnitudeY = Math.Abs(moveAmount.y);
        anim.SetFloat("MoveMagnitude", Math.Max(magnitudeX,magnitudeY));

    }

}
