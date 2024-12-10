using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraEnemies : MonoBehaviour
{
    [SerializeField] private int extraEnemyCap = 30;

    [SerializeField] private List<float> chances;

    private Dictionary<Enemy, int> extraEnemies = new Dictionary<Enemy, int>();
    private int totalExtraEnemies = 0;
    private int extraPrize = 100;

    public List<(int enemyType, int count)> rolledResults = new List<(int, int)>();

    public void AddRandomEnemies()
    {
        CheckChances();
        if (totalExtraEnemies >= extraEnemyCap)
        {
            Debug.Log("Enemy cap reached!");
            return;
        }

        rolledResults.Clear(); 

        for (int i = 0; i < 3; i++)
        {
            List<Enemy> listOfEnemies = GlobalManager.Instance.waveManager.EnemyTypes;
            int enemyType = Random.Range(0, listOfEnemies.Count);            
            int randomCount = RollEnemyCount();

            // IF I WANT THE CAP OF 30 TO BE STRICTLY RESPECTED
            // if (totalExtraEnemies + randomCount > extraEnemyCap)
            //     randomCount = extraEnemyCap - totalExtraEnemies;

            if (randomCount > 0)
            {
                Enemy randomEnemy = listOfEnemies[enemyType];
                // Add directly to WaveManager's enemiesToSpawn
                if (GlobalManager.Instance.waveManager.EnemiesToSpawn.ContainsKey(randomEnemy))
                {
                    GlobalManager.Instance.waveManager.EnemiesToSpawn[randomEnemy] += randomCount;
                }
                else
                {
                    GlobalManager.Instance.waveManager.EnemiesToSpawn[randomEnemy] = randomCount;
                }
            totalExtraEnemies += randomCount;
            rolledResults.Add((enemyType, randomCount));
            }            
            Debug.Log($"Extra enemies added: {randomCount}");
        }
        Debug.Log($"Extra prize: {extraPrize}");

        GlobalManager.Instance.waveManager.UpdatePrize(extraPrize);
    }

    public int RollEnemyCount()
    {
        if (chances == null || chances.Count == 0)
        {
            Debug.LogError("Chances list is not set or empty!");
            return 0;
        }

        float roll = Random.value;
        float cumulative = 0f;

        for (int i = 0; i < chances.Count; i++)
        {
            cumulative += chances[i];
            if (roll < cumulative)
                return i;
        }

        // in case of floating point errors:
        Debug.LogWarning("Roll exceeded cumulative chances. Returning last index.");
        return 1;
    }

    private void CheckChances()
    {
        float total = 0f;
        foreach (float chance in chances)
            total += chance;

        if (Mathf.Abs(total - 1f) > 0.0001f)
        {
            Debug.LogWarning("Chances do not sum up to 100%");
        }
    }

}
