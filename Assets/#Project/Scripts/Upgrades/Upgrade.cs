using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade : MonoBehaviour
{
    public int index;           
    public int maxLevel;     
    public int currentLevel = 0;  
    public int baseCost;   
    public int increment = 50; 
    public string description;    

    public PlayerData playerData;

    public void Start()
    {
        playerData = GlobalManager.Instance.GetComponent<PlayerData>();
    }

    public int GetCurrentCost()
    {
        return baseCost + (currentLevel-1)*increment;
    }

    public bool CanPurchase()
    {
        return currentLevel < maxLevel;
    }

    public virtual void Purchase()
    {
        if (CanPurchase())
        {
            currentLevel++;
        }
    }

    public abstract void ApplyEffect();

}

