using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade : MonoBehaviour
{
    public string upgradeName ;
    public int maxLevel;     
    public int baseCost;   
    public int increment = 50; 
    public string description;    
    public PlayerData playerData;
    public UpgradeData upgradeData;
    public int CurrentLevel
    {
        get
        {
            if (upgradeData != null && upgradeData.upgradeLevels.ContainsKey(upgradeName))
            {
                return upgradeData.upgradeLevels[upgradeName];
            }
            return 0; 
        }
    }

    protected virtual void Awake()
    {
        upgradeName = GetType().Name;
    }
    protected virtual void Start()
    {
        playerData = GlobalManager.Instance.GetComponent<PlayerData>();
        upgradeData = GlobalManager.Instance.GetComponent<UpgradeData>();
        upgradeData.ResetUpgrades();
    }

    public int GetCurrentCost()
    {
        return baseCost + (CurrentLevel * increment);
    }


    public bool CanPurchase()
    {
        return CurrentLevel < maxLevel;
    }

    public virtual void Purchase()
    {
        upgradeData.IncrementUpgradeLevel(upgradeName);
        Debug.Log($"{upgradeName} at level {CurrentLevel}/{maxLevel}");
        ApplyEffect();
    }

    public abstract void ApplyEffect();

    public void ResetUpgrade()
    {
        if (upgradeData.upgradeLevels.ContainsKey(upgradeName))
        {
            upgradeData.upgradeLevels[upgradeName] = 0;
        }
    }

}

