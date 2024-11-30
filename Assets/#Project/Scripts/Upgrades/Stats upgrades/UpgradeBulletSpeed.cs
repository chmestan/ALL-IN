using System.Collections.Generic;
using UnityEngine;

public class UpgradeBulletSpeed : StatUpgrade<int>
{
    protected override void Awake()
    {
        valuesByLevel = new Dictionary<int, int>
        {
            { 0, 13 },
            { 1, 15 },
            { 2, 17 },
            { 3, 20 }
        };
        base.Awake();
    }

    protected override void ApplyStatEffect(int value)
    {
        playerData.playerBulletSpeed = value;
        Debug.Log($"(UpgradeBulletSpeed) Bullet speed upgraded to {value}");
    }
}