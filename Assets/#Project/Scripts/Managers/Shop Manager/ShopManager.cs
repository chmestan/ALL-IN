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

            if (cost <= 0) Debug.LogError($"(ShopManager) {upgrade.upgradeName} has an invalid cost");
            else if (playerData.playerGold >= cost)
            {
                playerData.playerGold -= cost;
                upgrade.Purchase();

                Debug.Log($"(ShopManager) Purchased upgrade {upgrade.upgradeName} for {cost} gold.");
            }
            else
            {
                Debug.Log("(ShopManager) Not enough gold!");
            }
        }
        else
        {
            Debug.Log($"(ShopManager) Upgrade {upgrade.upgradeName} is already at max level!");
        }
    }

    public void AddEnemies()
    {
        
    }


}
