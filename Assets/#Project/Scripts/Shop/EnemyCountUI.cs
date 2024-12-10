using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyCountUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] enemyCountTexts; 
    private WaveManager waveManager;

    private void Start()
    {
        waveManager = GlobalManager.Instance.waveManager;
        UpdateEnemyCounts(); 
    }

    public void UpdateEnemyCounts()
    {
        if (waveManager.EnemiesToSpawn == null || waveManager.EnemiesToSpawn.Count == 0)
        {
            Debug.LogWarning("EnemiesToSpawn not initialized yet. Retrying...");
            StartCoroutine(WaitForEnemyData());
            return;
        }

        int i = 0;
        foreach (KeyValuePair<Enemy,int> enemyPair in waveManager.EnemiesToSpawn)
        {
            enemyCountTexts[i].text = $"{enemyPair.Value}"; 
            i++;
        }
    }

    private IEnumerator WaitForEnemyData()
    {
        yield return new WaitUntil(() => waveManager.EnemiesToSpawn != null && waveManager.EnemiesToSpawn.Count > 0);
        UpdateEnemyCounts();
    }
}
