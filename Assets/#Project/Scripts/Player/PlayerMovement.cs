using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    
        [SerializeField] private ArenaState arenaMgr;
        private InputDeviceHandler inputMgr;
        private Vector2 moveAmount;
        private Animator anim;

    [Header ("Movement"), Space (10f)]
        [SerializeField] float speed = 9f;
        [SerializeField] float acceleration = 30f;
        [SerializeField] float deceleration = 80f;
        [SerializeField] float turnSpeed = 1f;
        [SerializeField] float turnSpeedCompensation = 9f;
        public Vector2 lastDirection = new Vector2(1,0);
        private Vector2 currentVelocity = Vector2.zero;    

    [Header("Dash"), Space(10f)]
        [SerializeField] float dashSpeed = 20f; 
        [SerializeField] float dashDuration = 0.2f; 
        [SerializeField] float dashCooldown = 1f; 
        [SerializeField] float dashGracePeriod = 0.2f;
        [SerializeField] bool canDash = true;
        public bool isDashing = false;
        public bool isInGracePeriod = false;
        private Vector2 dashDirection;
    
    [Header("Flash Effect"), Space(10f)]
        [SerializeField] private Color flashColor = Color.yellow; 
        [SerializeField] private float flashDuration = 0.05f; 
        private SpriteRenderer spriteRenderer;
        private Color originalColor;

    [Header ("Technical"), Space (10f)]
        [SerializeField] float diagonalBufferTime = 0.1f;
        private float lastDirectionUpdateTime;


    [SerializeField, Space (20) ] bool debug = false;


    private void Awake()
    {
        inputMgr = GlobalManager.Instance.GetComponent<InputDeviceHandler>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            Move();
            UpdateLastDirection();
            AnimPlayer();
        }
    }
    private void Update()
    {
        if (inputMgr.dashInput.WasPressedThisFrame())
        {
            Dash();
        }
    }

    private void Move()
    {
        moveAmount = inputMgr.moveInput.ReadValue<Vector2>();

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

    public void Dash()
    {
        if (!canDash || isDashing || moveAmount == Vector2.zero) return;

        isDashing = true;
        canDash = false;

        dashDirection = moveAmount.normalized;

        StartCoroutine(PerformDash());
    }

private IEnumerator PerformDash()
{
    float elapsedTime = 0f;

    // Start Dash
    while (elapsedTime < dashDuration)
    {
        transform.Translate(dashDirection * dashSpeed * Time.deltaTime);
        elapsedTime += Time.deltaTime;
        yield return null;
    }

    isDashing = false; 
    isInGracePeriod = true; 

    yield return new WaitForSeconds(dashGracePeriod); 

    isInGracePeriod = false; 
    yield return new WaitForSeconds(dashCooldown - dashGracePeriod); 
    StartCoroutine(FlashEffect()); 
    canDash = true;
}

    private IEnumerator FlashEffect()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = flashColor; 
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.color = originalColor; 
        }
    }

}
