using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class UpgradeData : MonoBehaviour
{
    public Dictionary<string, int> upgradeLevels = new Dictionary<string, int>();
    public bool isInitialized = false;

    public void InitializeUpgrades()
    {
        if (isInitialized) return; 
        isInitialized = true;
        
        upgradeLevels.Clear();

        Upgrade[] upgrades = FindObjectsOfType<Upgrade>();

        foreach (Upgrade upgrade in upgrades)
        {
            if (upgrade.upgradeName == null)
            {
                Debug.LogError($"(UpgradeData) Upgrade name is null");
            }
            else if (upgradeLevels.ContainsKey(upgrade.upgradeName))
            {
                Debug.LogError($"(UpgradeData) {upgrade.upgradeName} appears more than once");
            }
            else
            {
                upgradeLevels.Add(upgrade.upgradeName, 0);
            }    
        }
    }

    public void IncrementUpgradeLevel(string upgradeName)
    {
        if (upgradeLevels.ContainsKey(upgradeName))
        {
            upgradeLevels[upgradeName]++;
            Debug.Log($"(PlayerData) Upgrade '{upgradeName}' incremented to level {upgradeLevels[upgradeName]}.");
        }
        else
        {
            Debug.LogError($"(PlayerData) Upgrade '{upgradeName}' not found in upgradeLevels.");
        }
    }

    public void ResetUpgradeLevels()
    {
        foreach (var key in new List<string>(upgradeLevels.Keys))
        {
            upgradeLevels[key] = 0;
            Debug.Log($"(UpgradeData) Upgrade '{key}' reset to level 0.");
        }

        Debug.Log("(UpgradeData) All upgrades have been reset.");
    }

}
