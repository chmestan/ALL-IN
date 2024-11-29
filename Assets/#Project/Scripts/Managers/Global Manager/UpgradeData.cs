using System.Collections.Generic;
using UnityEngine;

public class UpgradeData : MonoBehaviour
{
    public Dictionary<string, int> upgradeLevels = new Dictionary<string, int>();

    private void Start()
    {
        ResetUpgrades();
    }

    public void ResetUpgrades()
    {
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

}
