using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyTypeEnum
{
    Enemy1,
    Enemy2,
    Enemy3
}
public class EnemyPool : MonoBehaviour
{
    public static EnemyPool SharedInstance;
    public Dictionary<EnemyTypeEnum, List<GameObject>> enemyPools; 
    // types of enemies (key) - batch
    [SerializeField] List<GameObject> enemyPrefabs; 
    // THE ORDER OF PREFABS NEEDS TO MATCH THE ENEMY TYPES
    [SerializeField] int batchSize = 3; // in the future maybe I can make the batch size dependent to the type (class?)

    private void Awake()
    {
        SharedInstance = this;
        enemyPools = new Dictionary<EnemyTypeEnum, List<GameObject>>();
    }

    private void Start()
    {
        InitializePools();
    }

    private void InitializePools()
    {
        for (int i = 0; i < enemyPrefabs.Count; i++) // for each type of enemy
        {
            EnemyTypeEnum type = (EnemyTypeEnum)i; // equivalence between the type and its integer value 
            enemyPools[type] = new List<GameObject>(); // we create the entries in the dictionary: for each type its list of enemies (empty atm)
            CreateBatch(type, enemyPrefabs[i]); // we then go fill that list with prefabs of the corresponding index
            // which is why the prefabs list has to be in the correct order
        }
    }

    public GameObject GetPooledEnemy(EnemyTypeEnum type)
    {
        if (enemyPools.ContainsKey(type))
        {
            foreach (GameObject enemy in enemyPools[type])
            {
                if (!enemy.activeInHierarchy)
                {
                    return enemy;
                }
            }

            return AddEnemyToPool(type);
        }
        Debug.LogWarning($"No pool found for enemy type {type}");
        return null;
    }

    private void CreateBatch(EnemyTypeEnum type, GameObject prefab)
    {
        for (int i = 0; i < batchSize; i++) 
        {
            GameObject enemy = Instantiate(prefab);
            enemy.SetActive(false);
            enemyPools[type].Add(enemy); 
        }
    }

    private GameObject AddEnemyToPool(EnemyTypeEnum type) // if pool needs to be extended
    {
        GameObject prefab = enemyPrefabs[(int)type]; // get the prefab from the list with the type's int value
        GameObject enemy = Instantiate(prefab);
        enemy.SetActive(false);
        enemyPools[type].Add(enemy);
        return enemy;
    }
}

