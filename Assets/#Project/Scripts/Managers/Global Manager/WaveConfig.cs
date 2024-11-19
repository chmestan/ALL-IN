using System.Collections.Generic;
using UnityEngine;

public class WaveConfig : MonoBehaviour
{
    [Header("First Wave Configs"), Space (5f)]
        [SerializeField] int nbAvailableTypes1stWave = 3;
        [SerializeField] int nbEnemies1stWave = 4;
    
    // [SerializeField] private bool debug = false;

    private Dictionary<Enemy, int> enemiesToSpawn = new Dictionary<Enemy, int>();
    private int waveCount;
    public Dictionary<Enemy, int> EnemiesToSpawn
    {
        get => enemiesToSpawn;
        // set => enemiesToSpawn = value; 
    }

    private void Start()
    {
        enemiesToSpawn = FirstWave.Init(nbAvailableTypes1stWave,nbEnemies1stWave);
    }

}
