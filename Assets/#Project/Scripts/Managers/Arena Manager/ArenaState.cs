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
    public UnityEvent OnWaveCompleted = new UnityEvent();
    public UnityEvent OnWaveLost = new UnityEvent();
    private WaveManager waveManager;
    public PlayerData playerData;
    public PlayerHealth playerHealth;

    [SerializeField] private string shopSceneName = "ShopScene";
    [SerializeField] private float timeBeforeConfirm = 1f;

    private void Awake()
    {
        pauseScript = GetComponent<PauseGame>();
        enemyManager = GetComponent<EnemyManager>();
        changeScene = GlobalManager.Instance.GetComponent<ChangeScene>();
        inputMgr = GlobalManager.Instance.GetComponent<InputDeviceHandler>();
        waveManager = GlobalManager.Instance.GetComponent<WaveManager>();
        playerData = GlobalManager.Instance.GetComponent<PlayerData>();
    }

    private void Start()
    {
        waveManager.arenaState = this;
        playerData.arenaState = this;
        waveManager.arenaState.OnWaveCompleted.AddListener(waveManager.WaveCompletion);
        playerData.arenaState.OnWaveLost.AddListener(playerData.ResetGame);
        playerHealth = Player.Instance.GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            state = ArenaStateEnum.Lost;

            if (OnWaveLost != null)
            { 
                OnWaveLost.Invoke(); 
                Debug.Log("(ArenaState) Wave lost.");
            }
            else Debug.LogError("(ArenaState) OnWaveLost event is null");
        }

        if (enemyManager.RemainingEnemies <= 0 && state != ArenaStateEnum.Won)
        {
            state = ArenaStateEnum.Won;
                    
            if (OnWaveCompleted!= null) 
            {
                OnWaveCompleted.Invoke(); // we tell WaveManager to increment waveCounter
            }
            else Debug.LogError("(ArenaState) OnWaveCompleted event is null");

            Debug.Log("(ArenaState) Wave completed. Waiting for confirmation");
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
        yield return new WaitForSeconds(timeBeforeConfirm);

        while (!inputMgr.confirmInput.triggered)
        {
            yield return null; 
        }

        changeScene.LoadScene(shopSceneName);
    }

    private void OnDestroy()
    {
        waveManager.arenaState.OnWaveCompleted.RemoveAllListeners();
        playerData.arenaState.OnWaveLost.RemoveAllListeners();
    }
}
