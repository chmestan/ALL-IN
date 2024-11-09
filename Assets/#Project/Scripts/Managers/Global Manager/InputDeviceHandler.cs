using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputDeviceHandler : MonoBehaviour
{

    [Header("Input Map")]
    [SerializeField] InputActionAsset inputActions;
    public InputAction shootInput { get; private set; }
    public InputAction shootDirectionInput { get; private set; }

    public bool useGamepad;

    [SerializeField] private bool debug = false;

    private void Awake()
    {
        // Subscribe to device change events
        InputSystem.onDeviceChange += OnDeviceChange;

        DetectCurrentInputMode();
        SwitchInputMap();
    }

    private void OnDestroy()
    {
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
            if (debug) Debug.Log("[InputDeviceHandler] PlayerGamepad Input Map enabled");
            shootInput = inputActions.FindActionMap("PlayerGamepad").FindAction("Shoot");
            shootDirectionInput = inputActions.FindActionMap("PlayerGamepad").FindAction("Shoot Direction");
        }
        else
        {
            inputActions.FindActionMap("PlayerGamepad").Disable();
            inputActions.FindActionMap("PlayerKeyboard").Enable();
            if (debug) Debug.Log("[InputDeviceHandler] PlayerKeyboard Input Map enabled");
            shootInput = inputActions.FindActionMap("PlayerKeyboard").FindAction("Shoot");
            // shootDirectionInput = inputActions.FindActionMap("PlayerKeyboard").FindAction("Shoot Direction");
        }
    }

    public void EnableInputMap()
    {
        if (useGamepad)
            inputActions.FindActionMap("PlayerGamepad").Enable();
            if (debug) Debug.Log("[InputDeviceHandler] PlayerGamepad Input Map enabled");
        else
            inputActions.FindActionMap("PlayerKeyboard").Enable();
            if (debug) Debug.Log("[InputDeviceHandler] PlayerKeyboard Input Map enabled");
    }

    public void DisableInputMap()
    {
        inputActions.FindActionMap("PlayerKeyboard").Disable();
        inputActions.FindActionMap("PlayerGamepad").Disable();
    }
}
