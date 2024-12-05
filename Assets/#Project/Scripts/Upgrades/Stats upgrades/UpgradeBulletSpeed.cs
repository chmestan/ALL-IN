using System.Collections.Generic;
using UnityEngine;

public class UpgradeBulletSpeed : StatUpgrade<int>
{
    protected override void Awake()
    {
        valuesByLevel = new Dictionary<int, int>
        {
            { 0, 17 },
            { 1, 20 },
            { 2, 25 }
        };
        base.Awake();
    }

    protected override void ApplyStatEffect(int value)
    {
        playerData.playerBulletSpeed = value;
        Debug.Log($"(UpgradeBulletSpeed) Bullet speed upgraded to {value}");
    }
}