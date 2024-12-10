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
            { 0, 5 },
            { 1, 10 },
            { 2, 15 },
            { 3, 20 },
            { 4, 25 }
        };
        base.Awake();
    }

    protected override void ApplyStatEffect(int value)
    {
        playerData.playerMaxHealth = value;
        Debug.Log($"(UpgradeHealth) Health upgraded to {value}");
    }
}