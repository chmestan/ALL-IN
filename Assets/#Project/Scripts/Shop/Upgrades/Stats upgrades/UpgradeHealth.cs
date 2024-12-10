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
            { 0, 10 },
            { 1, 15 },
            { 2, 20 },
            { 3, 25 }
        };
        base.Awake();
    }

    protected override void ApplyStatEffect(int value)
    {
        playerData.playerMaxHealth = value;
        Debug.Log($"(UpgradeHealth) Health upgraded to {value}");
    }
}