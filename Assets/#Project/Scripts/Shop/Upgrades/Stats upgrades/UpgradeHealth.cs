using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeHealth : StatUpgrade<int>
{
    protected override void Awake()
    {
        valuesByLevel = new Dictionary<int, int>
        {
            { 0, 30 },
            { 1, 35 },
            { 2, 40 },
            { 3, 45 },
            { 4, 50 },
            { 5, 55 }
        };
        base.Awake();
    }

    protected override void ApplyStatEffect(int value)
    {
        playerData.playerMaxHealth = value;
        Debug.Log($"(UpgradeHealth) Health upgraded to {value}");
    }
}