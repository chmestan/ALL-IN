using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPools : MonoBehaviour
{
    public static EnemyPools SharedInstance;
    private Dictionary<EnemyBase, List<GameObject>> enemyPools = new Dictionary<EnemyBase, List<GameObject>>();    
    private List<EnemyBase> enemyTypes; 
    [SerializeField] int batchSize = 3; 

    public List<EnemyBase> EnemyTypes
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
        foreach (EnemyBase prefab in enemyTypes)
        {
            enemyPools[prefab] = new List<GameObject>();
            CreateBatch(prefab);
        }
    }

    public GameObject GetPooledEnemy(EnemyBase prefab)
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

    private void CreateBatch(EnemyBase prefab)
    {
        for (int i = 0; i < batchSize; i++) 
        {
            GameObject enemy = Instantiate(prefab.gameObject);
            enemy.SetActive(false);
            enemyPools[prefab].Add(enemy); 
        }
    }

    private GameObject AddEnemyToPool(EnemyBase prefab) // if pool needs to be extended
    {
        GameObject enemy = Instantiate(prefab.gameObject);
        enemy.SetActive(false);
        enemyPools[prefab].Add(enemy);
        return enemy;
    }
}

