using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ArenaState : MonoBehaviour
{
    public ArenaStateEnum state = ArenaStateEnum.Ongoing;
    private PauseGame pauseScript;
    private EnemyManager enemyManager;
    private InputDeviceHandler inputMgr;
    private ChangeScene changeScene;
    public UnityEvent OnWaveCompleted;
    public UnityEvent OnWaveLost;

    private bool temp = false;

    [SerializeField] private string shopSceneName = "ShopScene";

    private void Awake()
    {
        pauseScript = GetComponent<PauseGame>();
        enemyManager = GetComponent<EnemyManager>();
        changeScene = GlobalManager.Instance.GetComponent<ChangeScene>();
        inputMgr = GlobalManager.Instance.GetComponent<InputDeviceHandler>();
    }

    private void Update()
    {
        if (temp)
        {
            state = ArenaStateEnum.Lost;

            if (OnWaveLost != null) OnWaveLost.Invoke(); // !!!!!!!!!!!
            else Debug.LogError("(ArenaState) OnWaveLost event is null");
        }

        if (enemyManager.RemainingEnemies <= 0)
        {
            state = ArenaStateEnum.Won;
                    
            if (OnWaveCompleted!= null) OnWaveLost.Invoke(); // we tell WaveManager to increment waveCounter
            else Debug.LogError("(ArenaState) OnWaveCompleted event is null");

            Debug.Log("Wave completed. Waiting for player confirmation...");
            StartCoroutine(WaitForConfirm());
        }

        // we can only pause if the wave is ongoing, not if it's either won or lost
        else if (state == ArenaStateEnum.Ongoing) 
        {
            state = pauseScript.paused ? ArenaStateEnum.Paused : ArenaStateEnum.Ongoing;
        }
    }

    private IEnumerator WaitForConfirm()
    {
        // inputHandler.DisableInputMap();

        while (!inputMgr.confirmInput.triggered)
        {
            yield return null; 
        }
        
        Debug.Log("Player confirmed. Transitioning to the shop scene...");
        changeScene.LoadScene(shopSceneName);
    }
}
