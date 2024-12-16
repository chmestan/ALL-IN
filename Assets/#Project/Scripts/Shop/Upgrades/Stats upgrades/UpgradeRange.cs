using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeRange : StatUpgrade<int>
{
    protected override void Awake()
    {
        valuesByLevel = new Dictionary<int, int>
        {
            { 0, 7 },
            { 1, 8 },
            { 2, 9 },
            { 3, 10 },
            { 4, 12 },
            { 5, 15 }
        };
        base.Awake();
    }

    protected override void ApplyStatEffect(int value)
    {
        playerData.playerRange = value;
        Debug.Log($"(UpgradeRange) Range upgraded to {playerData.playerRange}");
    }

}
