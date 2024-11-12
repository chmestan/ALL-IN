using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletsPool : MonoBehaviour
{
    public static EnemyBulletsPool SharedInstance;
    public List<GameObject> enemyPooledBullets;
    [SerializeField] GameObject enemyBulletPrefab;
    [SerializeField] int batch;

    private void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        enemyPooledBullets = new List<GameObject>();
        CreateBatch();
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < enemyPooledBullets.Count; i++)
        {
            if(!enemyPooledBullets[i].activeInHierarchy)
            {
                return enemyPooledBullets[i];
            }
        }

        return AddObjectToPool();
    }

    void CreateBatch()
    {
        for(int i = 0; i < batch; i++)
        {
            GameObject tmp = Instantiate(enemyBulletPrefab);
            tmp.SetActive(false);
            enemyPooledBullets.Add(tmp);        
        }
    }

    private GameObject AddObjectToPool()
    {
        GameObject tmp = Instantiate(enemyBulletPrefab);
        tmp.SetActive(false);
        enemyPooledBullets.Add(tmp);
        return tmp;
    }

}
