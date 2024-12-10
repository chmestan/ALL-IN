using System.Collections.Generic;
using UnityEngine;


public class UpgradeDamage : StatUpgrade<int>
{
    protected override void Awake()
    {
        valuesByLevel = new Dictionary<int, int>
        {
            { 0, 5 },
            { 1, 7 },
            { 2, 10 },
            { 3, 15 }
        };
        base.Awake();
    }

    protected override void ApplyStatEffect(int value)
    {
        playerData.playerDamage = value;
        Debug.Log($"(UpgradeDamage) Damage upgraded to {value}");
    }
}