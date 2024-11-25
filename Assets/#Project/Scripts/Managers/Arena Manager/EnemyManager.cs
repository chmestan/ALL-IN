using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [Header ("SPAWNERS")]
    [SerializeField] private List<Transform> spawners;

    private WaveConfig waveConfig;
    private Dictionary<Enemy, int> enemiesToSpawn = new Dictionary<Enemy, int>();
    private Queue<Enemy> enemyQueue = new Queue<Enemy>();

    private int currentSpawnerIndex = 0;
    [Header ("ENEMIES"), Space (10f)]
    [SerializeField] private int activeEnemies = 0;
    [SerializeField] private int totalEnemies = 1;
    [SerializeField] private int killedEnemies = 0;

    [Header ("SPAWN RHYTHM"), Space (10f)]
    [SerializeField] private float delayTo5Enemies = 1f;
    [SerializeField] private float delayFrom5Enemies = 3f;

    private void Awake()
    {
        waveConfig = GlobalManager.Instance.GetComponent<WaveConfig>();
    }

    private void Start()
    {
        enemyQueue = GenerateEnemyQueue();
        totalEnemies = enemyQueue.Count;
        Debug.Log($"Total enemies to spawn: {totalEnemies}");
        StartCoroutine(SpawnEnemies());    
    }

    private void HandleEnemyDeath(Enemy enemy)
    {
        activeEnemies--;
        killedEnemies++;
        Debug.Log($"(EnemySpawner) Active: {activeEnemies}, Killed: {killedEnemies}");

        enemy.OnDeath.RemoveAllListeners();
    }


    private Queue<Enemy> GenerateEnemyQueue()
    {
        List<Enemy> tempList = new List<Enemy>();

        foreach (KeyValuePair<Enemy, int> entry in waveConfig.EnemiesToSpawn)
        {
            for (int i = 0; i < entry.Value; i++)
            {
                tempList.Add(entry.Key);
            }
        }

        ShuffleList(tempList); 
        return new Queue<Enemy>(tempList); 
    }

    private void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }


    private int CalculateTotalEnemies(Dictionary<Enemy, int> enemies)
    {
        int total = 0;
        foreach (int count in enemies.Values)
        {
            total += count;
        }
        return total;
    }
    private IEnumerator SpawnEnemies()
    {
        while (killedEnemies < totalEnemies)
        {
            if (enemyQueue.Count > 0)
            {
                if (activeEnemies < 5)
                {
                    SpawnEnemy(enemyQueue.Dequeue());
                    yield return new WaitForSeconds(delayTo5Enemies); 
                }
                else
                {
                    yield return new WaitForSeconds(delayFrom5Enemies); 
                }
            }
            else
            {
                yield return null; 
            }
        }

        Debug.Log("Wave Complete!");
    }
    private void SpawnEnemy(Enemy enemyPrefab)
    {
        GameObject enemy = EnemyPools.SharedInstance.GetPooledEnemy(enemyPrefab);
        if (enemy != null)
        {
            PlaceEnemy(enemy);

            enemy.SetActive(true);

            activeEnemies++;

            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            enemy.GetComponent<Enemy>().OnDeath.AddListener(() => HandleEnemyDeath(enemyComponent));        
        }
    }

    private void PlaceEnemy(GameObject enemy)
    {
            Transform spawnPoint = spawners[currentSpawnerIndex];
            enemy.transform.position = spawnPoint.position;
            currentSpawnerIndex ++;
    }



}




