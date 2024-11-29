using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeHealth : Upgrade
{
    private Dictionary<int, int> healthByLevel = new Dictionary<int, int>
    {
        { 0, 5 },
        { 1, 10 },
        { 2, 15 },
        { 3, 20 },
        { 4, 25 }
    };

    protected override void Start()
    {
        base.Start();
        maxLevel = healthByLevel.Count - 1;
    }

    public override void ApplyEffect()
    {
        int health;
        if (!healthByLevel.ContainsKey(CurrentLevel))
        {
            Debug.LogError($"(Upgrade) Upgrade level not included in dictionary...");
            return;
        }

        else health = healthByLevel[CurrentLevel];
        playerData.playerMaxHealth = health;

        Debug.Log($"(UpgradeHealth) Health upgraded to {playerData.playerMaxHealth} at level {CurrentLevel}");
    }


    // public override void ApplyEffect()
    // {
    //     playerData.playerMaxHealth += 5;
    // }
}
