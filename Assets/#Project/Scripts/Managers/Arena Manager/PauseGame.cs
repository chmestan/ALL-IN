using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class PauseGame : MonoBehaviour
{
        private InputDeviceHandler inputMgr;
        public bool paused;
        [SerializeField] private GameObject pauseScreen; 
        private AudioManager audioManager;
        [SerializeField] AudioClip pauseAudioClip;

    // [SerializeField, Space (20f)] private bool debug = false;

    private void Awake()
    {
        inputMgr = GlobalManager.Instance.GetComponent<InputDeviceHandler>();
    }

    private void Start()
    {
        audioManager = GlobalManager.Instance.GetComponentInChildren<AudioManager>();
    }

    private void Update()
    {

        Time.timeScale = WasPauseButtonPressed()? 0: 1;
        switch(Time.timeScale)
        {
            case 0:
                pauseScreen.SetActive(true);
                break;
            default:
                pauseScreen.SetActive(false);
                break;
        }
    }

    private bool WasPauseButtonPressed()
    {
        bool pauseInput = inputMgr.pauseInput.WasPressedThisFrame();
        if (pauseInput) 
        {
            paused = !paused;
            audioManager.PlaySFX(pauseAudioClip);
        }
        // if (debug) Debug.Log($"[PauseGame] Pause state = {paused}");

        return paused;
    }


}