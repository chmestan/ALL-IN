using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitPool : MonoBehaviour
{
    public static PitPool SharedInstance;
    public List<GameObject> pooledPits;
    [SerializeField] GameObject pitPrefab;
    [SerializeField] private Transform pitsParent;
    [SerializeField] int batch;

    private void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        pooledPits = new List<GameObject>();
        CreateBatch();
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < pooledPits.Count; i++)
        {
            if(!pooledPits[i].activeInHierarchy)
            {
                return pooledPits[i];
            }
        }

        return AddObjectToPool();
    }

    void CreateBatch()
    {
        for(int i = 0; i < batch; i++)
        {
            GameObject tmp = Instantiate(pitPrefab, pitsParent);
            tmp.SetActive(false);
            pooledPits.Add(tmp);        
        }
    }

    private GameObject AddObjectToPool()
    {
        GameObject tmp = Instantiate(pitPrefab, pitsParent);
        tmp.SetActive(false);
        pooledPits.Add(tmp);
        return tmp;
    }

}
