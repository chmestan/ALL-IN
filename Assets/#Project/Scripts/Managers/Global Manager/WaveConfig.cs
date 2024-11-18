using System.Collections.Generic;
using UnityEngine;

public class WaveConfig : MonoBehaviour
{
    [Header("First Wave Configs"), Space (5f)]
        [SerializeField] int nbAvailableTypes1stWave = 3;
        [SerializeField] int nbEnemies1stWave = 4;
    
    // [SerializeField] private bool debug = false;

    public Dictionary<EnemyBase, int> enemiesToSpawn = new Dictionary<EnemyBase, int>();
    private int waveCount;

    private void Start()
    {
        enemiesToSpawn = FirstWave.Init(nbAvailableTypes1stWave,nbEnemies1stWave);
    }



}
