using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header ("All enemies"), Space (10f)]
    [SerializeField] private List<Enemy> _EnemyTypes; // can I get all enemies automatically added from folder?

    [Header("First Wave Configs"), Space (5f)]
        private int nbAvailableTypes1stWave = 3;
        [SerializeField] int nbEnemies1stWave = 4;
    
    private Dictionary<Enemy, int> enemiesToSpawn = new Dictionary<Enemy, int>();

    [Header ("Wave Progress"), Space (10f)]
    [SerializeField] private int waveCount;

    [SerializeField] GameObject arenaManager;  
    private ArenaState arenaState;  

    #region Properties
    public List<Enemy> EnemyTypes
    { get => _EnemyTypes;}
    public Dictionary<Enemy, int> EnemiesToSpawn
    { get => enemiesToSpawn;}
    // public int WaveCount
    // { get => _WaveCount;}
    #endregion

    private void Start()
    {
        enemiesToSpawn = FirstWave.Init(nbAvailableTypes1stWave,nbEnemies1stWave);
        if (arenaManager != null) 
        {
            arenaState = arenaManager.GetComponent<ArenaState>();
            if (arenaState != null)
            {
                arenaState.OnWaveCompleted.AddListener(IncrementWaveCount); // subscribe to event arena completion (coming from Arena State)
            }
            else Debug.LogError("(WaveManager) Arena State component can't be found");
        }
        else Debug.LogError("(WaveManager) Arena Manager can't be found");

    }

    private void IncrementWaveCount()
    {
        waveCount++;
        Debug.Log($"(WaveManager) Wave Count incremented: {waveCount}");
    }

    private void OnDestroy()
    {
        if (arenaState != null) arenaState.OnWaveCompleted.RemoveListener(IncrementWaveCount);
    }

}
