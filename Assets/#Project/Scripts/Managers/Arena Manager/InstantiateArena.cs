using System.Collections.Generic;
using UnityEngine;

public class InstantiateArena : MonoBehaviour
{
    private WaveConfig waveConfig;
    private Dictionary<EnemyBase, int> enemiesToSpawn = new Dictionary<EnemyBase, int>();

    private void Awake()
    {
        waveConfig = GlobalManager.Instance.GetComponent<WaveConfig>();
        enemiesToSpawn = waveConfig.enemiesToSpawn;
    }

    private void Start()
    {
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
                    Vector3 spawnPosition = GetRandomSpawnPosition(); // temporary
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
