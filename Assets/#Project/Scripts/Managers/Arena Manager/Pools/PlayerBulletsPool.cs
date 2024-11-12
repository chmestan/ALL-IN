using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletsPool : MonoBehaviour
{
    public static PlayerBulletsPool SharedInstance;
    public List<GameObject> pooledPlayerBullets;
    [SerializeField] GameObject playerBulletPrefab;
    [SerializeField] int batch;

    private void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        pooledPlayerBullets = new List<GameObject>();
        CreateBatch();
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < pooledPlayerBullets.Count; i++)
        {
            if(!pooledPlayerBullets[i].activeInHierarchy)
            {
                return pooledPlayerBullets[i];
            }
        }

        return AddObjectToPool();
    }

    void CreateBatch()
    {
        for(int i = 0; i < batch; i++)
        {
            GameObject tmp = Instantiate(playerBulletPrefab);
            tmp.SetActive(false);
            pooledPlayerBullets.Add(tmp);        
        }
    }

    private GameObject AddObjectToPool()
    {
        GameObject tmp = Instantiate(playerBulletPrefab);
        tmp.SetActive(false);
        pooledPlayerBullets.Add(tmp);
        return tmp;
    }

}
