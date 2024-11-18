using System.Collections.Generic;
using UnityEngine;

public static class FirstWave
{
    public static List<EnemyBase> availableEnemies = new List<EnemyBase>();
    public static Dictionary<EnemyBase, int> enemiesToSpawn = new Dictionary<EnemyBase, int>();
    private static List<EnemyBase> enemyTypes;

    public static Dictionary<EnemyBase, int> Init(int nbAvailableTypes, int nbEnemiesToGenerate)
    {
        AvailableEnemies1stWave(nbAvailableTypes);
        return EnemiesToGenerate1stWave(nbEnemiesToGenerate);
    }

    private static void AvailableEnemies1stWave(int nbAvailableTypes)
    {
        enemyTypes = EnemyPools.SharedInstance.enemyTypes;

        // Select the first X enemy types for the first wave
        availableEnemies.Clear();
        for (int i = 0; i < nbAvailableTypes; i++)
        {
            availableEnemies.Add(enemyTypes[i]);
        }
    }

    public static Dictionary<EnemyBase, int> EnemiesToGenerate1stWave(int nbEnemiesToGenerate)
    {
        List<(int a, int b, int c)> combinations = Combinations(nbEnemiesToGenerate);

        (int countA, int countB, int countC) = combinations[Random.Range(0, combinations.Count)];

        enemiesToSpawn.Clear();
        enemiesToSpawn[availableEnemies[0]] = countA;
        enemiesToSpawn[availableEnemies[1]] = countB;
        enemiesToSpawn[availableEnemies[2]] = countC;
        
        Debug.Log($"Enemies to spawn: {availableEnemies[0].name}: {countA}, {availableEnemies[1].name}: {countB}, {availableEnemies[2].name}: {countC}");

        return enemiesToSpawn;
    }

    private static List<(int a, int b, int c)> Combinations(int nb)
    {
        List<(int a, int b, int c)> combinations = new List<(int, int, int)>();

        // All possible combinations for three enemy types
        for (int a = 0; a <= nb; a++)
        {
            for (int b = 0; b <= nb - a; b++)
            {
                int c = nb - a - b;
                if (c >= 0) combinations.Add((a, b, c));
            }
        }

        return combinations;
    }
}
