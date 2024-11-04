using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InstantiateArena : MonoBehaviour
{

    Dictionary<EnemyTypeEnum, int> enemiesToSpawn = new Dictionary<EnemyTypeEnum, int>
    {
        { EnemyTypeEnum.Enemy0, 2 },
        { EnemyTypeEnum.Enemy1, 3 },
        { EnemyTypeEnum.Enemy2, 1 }
    };

    private void Start()
    {
        InstantiateEnemies(enemiesToSpawn);
    }

    private void InstantiateEnemies(Dictionary<EnemyTypeEnum, int> enemiesToSpawn)
    {
        foreach (KeyValuePair<EnemyTypeEnum, int> entry in enemiesToSpawn)
        {
            for (int _ = 0 ; _ < entry.Value ; _++)
            {
                GameObject enemy = EnemyPools.SharedInstance.GetPooledEnemy(entry.Key); 
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
        float z = 0;
        return new Vector3(x, y, z);
    }


}
