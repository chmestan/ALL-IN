using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header ("All enemies"), Space (3f)]
        [SerializeField] private List<Enemy> enemyTypes; // can I get all enemies automatically added from folder?
        private Dictionary<Enemy, int> enemiesToSpawn;

    [Header ("Wave Progress"), Space (3f)]
        [SerializeField] private int waveCount = 1;

    [Header ("References"), Space (3f)]
        public ArenaState arenaState;  
        public EnemyManager enemyManager;
        public PlayerData playerData;

    private int prize = 100;
    public int Prize 
    { get => prize;
      set => prize = value;
    }

    #region Properties
    public List<Enemy> EnemyTypes
    { get => enemyTypes;}
    public Dictionary<Enemy, int> EnemiesToSpawn
    { get => enemiesToSpawn;}
    public int WaveCount
    { get => waveCount;
      set => waveCount = value;}
    #endregion

    [Header("Debug"), Space(3f)]
        [SerializeField] private bool debug = false;

    private void Start()
    {
        playerData = GlobalManager.Instance.GetComponent<PlayerData>();
        FirstWave();
    }

    public void FirstWave()
    {
        enemiesToSpawn = EnemyDictionaryManager.CreateEnemyDictionary(waveCount, enemyTypes.Count);
    }

    public void WaveCompletion()
    {
        IncrementWaveCount();
        MoneyReward();
        NextWaveDefaultConfig();
    }

    public void IncrementWaveCount()
    {
        waveCount++;
        if (debug) Debug.Log($"(WaveManager) Wave Count incremented: {waveCount}");
    }

    public void MoneyReward()
    {
        playerData.playerGold += prize;
    }

    public void NextWaveDefaultConfig()
    {
        prize = 100;
        enemiesToSpawn = EnemyDictionaryManager.CreateEnemyDictionary(waveCount, enemyTypes.Count);
    }

    public void UpdatePrize(int extraPrize)
    {
        prize += extraPrize;
        if (debug) Debug.Log($"Updated prize: {prize}");
    }

}
