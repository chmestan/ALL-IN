using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPChipPool : MonoBehaviour
{

    public static HPChipPool SharedInstance;
    public List<GameObject> chipsPooled;
    [SerializeField] GameObject chipPrefab;
    [SerializeField] int batch = 15;

    private void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        chipsPooled = new List<GameObject>();
        CreateBatch();
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < chipsPooled.Count; i++)
        {
            if(!chipsPooled[i].activeInHierarchy)
            {
                return chipsPooled[i];
            }
        }

        return AddObjectToPool();
    }

    void CreateBatch()
    {
        for(int i = 0; i < batch; i++)
        {
            GameObject tmp = Instantiate(chipPrefab);
            tmp.SetActive(false);
            chipsPooled.Add(tmp);        
        }
    }

    private GameObject AddObjectToPool()
    {
        GameObject tmp = Instantiate(chipPrefab);
        tmp.SetActive(false);
        chipsPooled.Add(tmp);
        return tmp;
    }


}
