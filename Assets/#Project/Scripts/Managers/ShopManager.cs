using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public PlayerData playerData;

    public void Start()
    {
        playerData = GlobalManager.Instance.GetComponent<PlayerData>();
    }


    public void TryPurchaseUpgrade(Upgrade upgrade)
    {
        if (upgrade.CanPurchase())
        {
            int cost = upgrade.GetCurrentCost();

            if (playerData.playerGold >= cost)
            {
                playerData.playerGold -= cost;
                upgrade.Purchase();
                upgrade.ApplyEffect();

                Debug.Log($"(ShopManager) Purchased upgrade {upgrade.index} (Level {upgrade.currentLevel}/{upgrade.maxLevel}) for {cost} gold.");
            }
            else
            {
                Debug.Log("(ShopManager) Not enough gold!");
            }
        }
        else
        {
            Debug.Log($"(ShopManager) Upgrade {upgrade.index} is already at max level!");
        }
    }

    public void AddEnemies()
    {
        
    }


}
