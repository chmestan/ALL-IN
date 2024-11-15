using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveConfig : MonoBehaviour
{
    [Header("First Wave Configs")]
        [SerializeField] int nbTypesAvailable1stWave = 3;
        [SerializeField] int nbEnemies1stWave = 4;
        [SerializeField] protected List<EnemyBase> availableEnemies = new List<EnemyBase>();
    private int waveCount;
    private List<EnemyBase> enemyTypes;
    public Dictionary<EnemyBase, int> enemiesToSpawn = new Dictionary<EnemyBase, int>();


    private void Start()
    {
        Init1stWave();

    }

    private void Init1stWave()
    {
        AvailableEnemies1stWave();
        EnemiesToGenerate1stWave();
    }

    private void AvailableEnemies1stWave()
    {
        enemyTypes = EnemyPools.SharedInstance.enemyTypes;
        for (int i = 0; i < nbTypesAvailable1stWave ; i++) 
        {
            availableEnemies.Add(enemyTypes[i]);
        }
    }

    private void EnemiesToGenerate1stWave()
    {
        List<(int a, int b, int c)> combinations = Combinations(nbEnemies1stWave);

        (int countA, int countB, int countC) = combinations[Random.Range(0, combinations.Count)];

        enemiesToSpawn[availableEnemies[0]] = countA;
        enemiesToSpawn[availableEnemies[1]] = countB;
        enemiesToSpawn[availableEnemies[2]] = countC;

        Debug.Log($"Enemies to spawn: {availableEnemies[0].name}: {countA}, {availableEnemies[1].name}: {countB}, {availableEnemies[2].name}: {countC}");
            
    }

    private List<(int a, int b, int c)>  Combinations(int total) // hard coded 3 different enemy types possibilities
    {
        List<(int a, int b, int c)> combinations = new List<(int, int, int)>();

        for (int a = 0; a <= total; a++)
        {
            for (int b = 0; b <= total - a ; b++)
            {
                int c = total - a - b;

                if (c >= 0) combinations.Add((a, b, c));
            }
        }

        return combinations;
    }


}