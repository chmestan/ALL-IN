using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool SharedInstance;
    public List<GameObject> pooledBullets;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int batch;

    private void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        pooledBullets = new List<GameObject>();
        CreateBatch();
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < pooledBullets.Count; i++)
        {
            if(!pooledBullets[i].activeInHierarchy)
            {
                return pooledBullets[i];
            }
        }

        return AddObjectToPool();
    }

    void CreateBatch()
    {
        for(int i = 0; i < batch; i++)
        {
            GameObject tmp = Instantiate(bulletPrefab);
            tmp.SetActive(false);
            pooledBullets.Add(tmp);        
        }
    }

    private GameObject AddObjectToPool()
    {
        GameObject tmp = Instantiate(bulletPrefab);
        tmp.SetActive(false);
        pooledBullets.Add(tmp);
        return tmp;
    }

}
