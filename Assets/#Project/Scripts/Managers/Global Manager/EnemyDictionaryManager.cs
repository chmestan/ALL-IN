using System.Collections.Generic;
using UnityEngine;

public static class EnemyDictionaryManager 
{
    private static Dictionary<Enemy, int> enemiesToSpawn = new Dictionary<Enemy, int>();
    private static List<Enemy> enemyTypes;

    public static Dictionary<Enemy, int> CreateEnemyDictionary(int waveNb, int nbAvailableTypes)
    {
        enemyTypes = GlobalManager.Instance.waveManager.EnemyTypes;

        int totalEnemiesToGenerate = GetNumberOfEnemies(waveNb);

        enemiesToSpawn.Clear();
        GenerateListOfEnemies(nbAvailableTypes, totalEnemiesToGenerate);
        // might want to add a difference between available types and all types later

        return enemiesToSpawn;
    }

    private static int GetNumberOfEnemies(int waveNb) // Example of scaling logic
    {
        return (waveNb + 1) * 2; 
    }

    private static void GenerateListOfEnemies(int nbAvailableTypes, int nbEnemiesToGenerate)
    {
        enemiesToSpawn.Clear(); 
        foreach (Enemy enemy in enemyTypes.GetRange(0, nbAvailableTypes))
        {
            enemiesToSpawn[enemy] = 0; 
        }

        List<Enemy> randomEnemyList = new List<Enemy>();

        for (int i = 0; i < nbEnemiesToGenerate; i++)
        {
            // random type amongst ALL types -- might want to change later
            Enemy randomEnemy = enemyTypes[Random.Range(0, nbAvailableTypes)];
            randomEnemyList.Add(randomEnemy);
        }

        foreach (Enemy enemy in randomEnemyList)
        {
            enemiesToSpawn[enemy]++;
        }

    }
}
