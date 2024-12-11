using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Upgrade : MonoBehaviour
{
    public string upgradeName ;
    public int maxLevel;     
    public int baseCost;   
    public int increment = 50; 
    public string description;    
    public PlayerData playerData;
    public UpgradeData upgradeData;
    private TextMeshProUGUI levelText;
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
        levelText = GetComponentInChildren<TextMeshProUGUI>();
    }

    protected virtual void Start()
    {
        playerData = GlobalManager.Instance.GetComponent<PlayerData>();
        upgradeData = GlobalManager.Instance.GetComponent<UpgradeData>();
        upgradeData.InitializeUpgrades();

        UpdateLevelText(); 
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
        UpdateLevelText();
    }

    public abstract void ApplyEffect();

    // public void ResetUpgrade()
    // {
    //     if (upgradeData.upgradeLevels.ContainsKey(upgradeName))
    //     {
    //         upgradeData.upgradeLevels[upgradeName] = 0;
    //         Debug.Log($"{upgradeName} reset to level {upgradeData.upgradeLevels[upgradeName]}");
    //     }
    // }

    private void UpdateLevelText()
    {
        if (levelText != null)
        {
            levelText.text = $"{CurrentLevel}";
        }
        else
        {
            Debug.LogWarning($"{upgradeName}: Level text is not assigned!");
        }
    }    

}

