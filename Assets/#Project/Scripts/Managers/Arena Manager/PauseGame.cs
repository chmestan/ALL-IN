using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class PauseGame : MonoBehaviour
{
    [Header ("Input Map")]
        private InputDeviceHandler inputMgr;

        public bool paused;

    // [SerializeField, Space (20f)] private bool debug = false;

    private void Awake()
    {
        inputMgr = GlobalManager.Instance.GetComponent<InputDeviceHandler>();
    }

    private void Update()
    {
        Time.timeScale = WasPauseButtonPressed()? 0: 1;
    }

    private bool WasPauseButtonPressed()
    {
        bool pauseInput = inputMgr.pauseInput.WasPressedThisFrame();
        if (pauseInput) paused = !paused;

        // if (debug) Debug.Log($"[PauseGame] Pause state = {paused}");

        return paused;
    }


}