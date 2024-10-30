using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool SharedInstance;
    public List<GameObject> pooledBullets;
    [SerializeField] GameObject bulletsToPool;
    [SerializeField] int batch;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledBullets = new List<GameObject>();
        GameObject tmp;
        for(int i = 0; i < batch; i++)
        {
            tmp = Instantiate(bulletsToPool);
            tmp.SetActive(false);
            pooledBullets.Add(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < batch; i++)
        {
            if(!pooledBullets[i].activeInHierarchy)
            {
                return pooledBullets[i];
            }
        }
        return null;
}

}
