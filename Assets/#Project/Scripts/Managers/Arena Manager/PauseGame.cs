using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class PauseGame : MonoBehaviour
{
    [Header ("Input Map")]
    [SerializeField] InputActionAsset inputActions;
    private InputAction pause;
    public bool paused;

    // [SerializeField, Space (20f)] private bool debug = false;

    private void Awake()
    {
        pause = inputActions.FindActionMap("Player").FindAction("Pause");
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
        Time.timeScale = WasPauseButtonPressed()? 0: 1;
    }

    private bool WasPauseButtonPressed()
    {
        bool pauseInput = pause.WasPressedThisFrame();
        if (pauseInput) paused = !paused;

        // if (debug) Debug.Log($"[PauseGame] Pause state = {paused}");

        return paused;
    }


}