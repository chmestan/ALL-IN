using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header ("All enemies"), Space (3f)]
    [SerializeField] private List<Enemy> enemyTypes; // can I get all enemies automatically added from folder?

    private Dictionary<Enemy, int> enemiesToSpawn;

    [Header ("Wave Progress"), Space (3f)]
    [SerializeField] private int waveCount = 1;

    [Header ("Money Reward"), Space (3f)]
    [SerializeField] private int prize = 100; // always 100 by default
    
    public ArenaState arenaState;  
    public EnemyManager enemyManager;
    public PlayerData playerData;

    #region Properties
    public List<Enemy> EnemyTypes
    { get => enemyTypes;}
    public Dictionary<Enemy, int> EnemiesToSpawn
    { get => enemiesToSpawn;}
    public int WaveCount
    { get => waveCount;}
    #endregion

    private void Start()
    {
        playerData = GlobalManager.Instance.GetComponent<PlayerData>();

        enemiesToSpawn = EnemyDictionaryManager.CreateEnemyDictionary(waveCount, enemyTypes.Count);

        // add more enemies if added
        // add prize money if enemies added

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
        Debug.Log($"(WaveManager) Wave Count incremented: {waveCount}");
    }

    public void MoneyReward()
    {
        playerData.playerGold += prize;
    }

    public void NextWaveDefaultConfig()
    {
        enemiesToSpawn = EnemyDictionaryManager.CreateEnemyDictionary(waveCount, enemyTypes.Count);
    }

    public void UpdateExtraEnemies(Dictionary<Enemy, int> extraEnemies, int extraPrize)
    {
        foreach (var entry in extraEnemies)
        {
            if (enemiesToSpawn.ContainsKey(entry.Key))
                enemiesToSpawn[entry.Key] += entry.Value;
            else
                enemiesToSpawn[entry.Key] = entry.Value;
        }

        prize += extraPrize;
    }

}
