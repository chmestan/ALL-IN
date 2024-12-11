using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [Header ("SPAWNERS")]
    [SerializeField] private List<Transform> spawners;

    private WaveManager waveManager;

    private Dictionary<Enemy, int> enemiesToSpawn = new Dictionary<Enemy, int>();
    private Queue<Enemy> enemyQueue = new Queue<Enemy>();

    private int currentSpawnerIndex;
    [Header ("ENEMIES"), Space (10f)]
    [SerializeField] private int activeEnemies = 0;
    [SerializeField] private int totalEnemies;
    [SerializeField] private int killedEnemies = 0;
    [SerializeField] private int remainingEnemies;
    public int RemainingEnemies
    {
        get => remainingEnemies;
    }

    [Header ("SPAWN RHYTHM"), Space (10f)]
    [SerializeField] private float delayTo5Enemies = 1f;
    [SerializeField] private float delayFrom5Enemies = 4f;

    private void Awake()
    {
        waveManager = GlobalManager.Instance.GetComponent<WaveManager>();
        currentSpawnerIndex = Random.Range(0,4);
    }

    private void Start()
    {
        InitializeWave();
        StartCoroutine(SpawnEnemies());    
    }

    private void InitializeWave()
    {
        enemyQueue = GenerateEnemyQueue();
        totalEnemies = enemyQueue.Count;
        killedEnemies = 0;
        activeEnemies = 0;
        remainingEnemies = totalEnemies;
        waveManager.enemyManager = this;
    }

    private void HandleEnemyDeath(Enemy enemy)
    {
        activeEnemies--;
        killedEnemies++;
        remainingEnemies = totalEnemies - killedEnemies;
        Debug.Log($"(EnemySpawner) Active: {activeEnemies}, Killed: {killedEnemies}");

        enemy.OnDeath.RemoveAllListeners();
    }


    private Queue<Enemy> GenerateEnemyQueue() 
    {
        List<Enemy> tempList = new List<Enemy>();

        foreach (KeyValuePair<Enemy, int> entry in waveManager.EnemiesToSpawn)
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

    private IEnumerator SpawnEnemies()
    {
        while (killedEnemies < totalEnemies)
        {
            if (enemyQueue.Count > 0)
            {
                if (activeEnemies < 4)
                {
                    SpawnEnemy(enemyQueue.Dequeue());
                    yield return new WaitForSeconds(delayTo5Enemies);
                }
                else
                {
                    yield return new WaitForSeconds(delayFrom5Enemies);
                        SpawnEnemy(enemyQueue.Dequeue());
                }
            }
            else
            {
                yield return null;
            }
        }

        Debug.Log("(Enemy Manager) Wave Complete!");
    }

    private void SpawnEnemy(Enemy enemyPrefab)
    {
        GameObject enemy = EnemyPools.SharedInstance.GetPooledEnemy(enemyPrefab);
        if (enemy != null)
        {
            enemy.SetActive(true);
            PlaceEnemy(enemy);


            activeEnemies++;

            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            enemy.GetComponent<Enemy>().OnDeath.AddListener(() => HandleEnemyDeath(enemyComponent));        
        }
    }

    private void PlaceEnemy(GameObject enemy)
    {
            Transform spawnPoint = spawners[currentSpawnerIndex];
            enemy.transform.position = spawnPoint.position;
            currentSpawnerIndex = (currentSpawnerIndex +1) % spawners.Count;
    }

    public void NextWave()
    {
        StopAllCoroutines();
        InitializeWave();   //reinitialize wave-specific variables
        StartCoroutine(SpawnEnemies());
    }

}




