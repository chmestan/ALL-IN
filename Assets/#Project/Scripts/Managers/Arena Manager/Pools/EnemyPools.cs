using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPools : MonoBehaviour
{
    public static EnemyPools SharedInstance;
    private Dictionary<Enemy, List<GameObject>> enemyPools = new Dictionary<Enemy, List<GameObject>>();    
    private List<Enemy> enemyTypes; 
    [SerializeField] int batchSize = 3; 

    public List<Enemy> EnemyTypes
    {
        get => enemyTypes;
    }

    private void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        InitializePools();
    }

    private void InitializePools()
    {
        foreach (Enemy prefab in enemyTypes)
        {
            enemyPools[prefab] = new List<GameObject>();
            CreateBatch(prefab);
        }
    }

    public GameObject GetPooledEnemy(Enemy prefab)
    {
        if (enemyPools.ContainsKey(prefab))
        {
            foreach (GameObject enemy in enemyPools[prefab])
            {
                if (!enemy.activeInHierarchy)
                {
                    return enemy;
                }
            }

            return AddEnemyToPool(prefab);
        }
        Debug.LogWarning($"No pool found for enemy type {prefab.name}");
        return null;
    }

    private void CreateBatch(Enemy prefab)
    {
        for (int i = 0; i < batchSize; i++) 
        {
            GameObject enemy = Instantiate(prefab.gameObject);
            enemy.SetActive(false);
            enemyPools[prefab].Add(enemy); 
        }
    }

    private GameObject AddEnemyToPool(Enemy prefab) // if pool needs to be extended
    {
        GameObject enemy = Instantiate(prefab.gameObject);
        enemy.SetActive(false);
        enemyPools[prefab].Add(enemy);
        return enemy;
    }
}

