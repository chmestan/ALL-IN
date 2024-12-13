using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ArenaState : MonoBehaviour
{
    public ArenaStateEnum state = ArenaStateEnum.Ongoing;
    public bool started = false;
    
    #region References
        private PauseGame pauseScript;
        private EnemyManager enemyManager;
        private InputDeviceHandler inputMgr;
        private ChangeScene changeScene;
        private WaveManager waveManager;
        public PlayerData playerData;
        public PlayerHealth playerHealth;
    #endregion

    #region Events
        public UnityEvent OnWaveCompleted = new UnityEvent();
        public UnityEvent OnWaveLost = new UnityEvent();
    #endregion

    [Header ("UI"), Space (3f)]
        [SerializeField] private GameObject waveWonUI;
        [SerializeField] private GameObject waveLostUI;
        [SerializeField] private float timeBeforeConfirm = 1f;

    [Header("Audio"), Space(3f)]
        private AudioManager audioManager;
        [SerializeField] AudioClip waveClearedAudioClip;

    [Header ("Debug"), Space (3f)]
        [SerializeField] private bool debug = false;

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
        audioManager = GlobalManager.Instance.GetComponentInChildren<AudioManager>();
        playerData.arenaState.OnWaveLost.AddListener(playerData.ResetGame);
        playerHealth = Player.Instance.GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (playerHealth.isDead == true)
        {
            if (OnWaveLost != null)
            { 
                OnWaveLost.Invoke(); 
                playerData.arenaState.OnWaveLost.RemoveAllListeners();
                StartCoroutine(WaitForConfirm("MenuScene",waveLostUI));
                // Debug.Log("(ArenaState) Wave lost.");
            }
            else Debug.LogError("(ArenaState) OnWaveLost event is null");
        }

        else if (enemyManager.RemainingEnemies <= 0 && state != ArenaStateEnum.Won)
        {
            state = ArenaStateEnum.Won;
                    
            if (OnWaveCompleted!= null) 
            {
                OnWaveCompleted.Invoke(); // we tell WaveManager to increment waveCounter
                StartCoroutine(WaitForConfirm("ShopScene", waveWonUI));
            }
            else Debug.LogError("(ArenaState) OnWaveCompleted event is null");

        }

        // we can only pause if the wave is ongoing, not if it's either won or lost
        else if (state == ArenaStateEnum.Ongoing) 
        {
            state = pauseScript.paused ? ArenaStateEnum.Paused : ArenaStateEnum.Ongoing;
        }
    }

    private IEnumerator WaitForConfirm(string nextScene, GameObject ui)
    {
        yield return new WaitForSeconds(timeBeforeConfirm);

        ui.SetActive(true);
        audioManager.FadeMusicGroup(0.0001f, 0.5f);
        audioManager.PlaySFX(waveClearedAudioClip);

        if (debug) Debug.Log("(ArenaState) Waiting for player confirmation.");
        while (!inputMgr.confirmInput.triggered)
        {
            yield return null;
        }

        changeScene.LoadSceneWithTransition(nextScene, 1f);
    }


    private void OnDestroy()
    {
        waveManager.arenaState.OnWaveCompleted.RemoveAllListeners();
        playerData.arenaState.OnWaveLost.RemoveAllListeners();
    }
}
