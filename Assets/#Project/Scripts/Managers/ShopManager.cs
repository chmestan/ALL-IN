using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{

    [Header("Player Upgrades")]
    [SerializeField] private int playerDamageUpgradeCost = 100;
    [SerializeField] private int playerHealthUpgradeCost = 150;

    [Header("Wave Progression")]
    [SerializeField] private string gameplaySceneName = "GameplayScene";

    private int currentGold;


}
