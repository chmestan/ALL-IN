using System.Collections.Generic;
using UnityEngine;

public class InstantiateArena : MonoBehaviour
{
    private List<EnemyBase> enemyPrefabs; 
    [SerializeField] Dictionary<EnemyBase, int> enemiesToSpawn;

    private void Start()
    {
        enemyPrefabs = EnemyPools.SharedInstance.enemyPrefabs;

        InstantiateEnemies(enemiesToSpawn);
    }

    private void InstantiateEnemies(Dictionary<EnemyBase, int> enemiesToSpawn)
    {
        foreach (KeyValuePair<EnemyBase, int> entry in enemiesToSpawn)
        {
            EnemyBase enemyPrefab = entry.Key;
            int count = entry.Value;

            for (int i = 0; i < count; i++)
            {
                GameObject enemy = EnemyPools.SharedInstance.GetPooledEnemy(enemyPrefab);
                if (enemy != null)
                {
                    Vector3 spawnPosition = GetRandomSpawnPosition();
                    enemy.transform.position = spawnPosition;
                    enemy.SetActive(true);
                }
            }
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float x = Random.Range(-10, 10);
        float y = Random.Range(-10, 10);
        return new Vector3(x, y, 0);
    }
}
