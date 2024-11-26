using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header ("All enemies"), Space (10f)]
    [SerializeField] private List<Enemy> enemyTypes; // can I get all enemies automatically added from folder?

    private Dictionary<Enemy, int> enemiesToSpawn;

    [Header ("Wave Progress"), Space (10f)]
    [SerializeField] private int waveCount = 1;
    
    public ArenaState arenaState;  
    public EnemyManager enemyManager;


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
        enemiesToSpawn = EnemyDictionaryManager.CreateEnemyDictionary(waveCount, enemyTypes.Count);
    }

    public void IncrementWaveCount()
    {
        waveCount++;
        Debug.Log($"(WaveManager) Wave Count incremented: {waveCount}");
    }

    public void NextWaveDefaultConfig()
    {
        enemiesToSpawn = EnemyDictionaryManager.CreateEnemyDictionary(waveCount, enemyTypes.Count);
        // enemyManager.NextWave();
    }


}
