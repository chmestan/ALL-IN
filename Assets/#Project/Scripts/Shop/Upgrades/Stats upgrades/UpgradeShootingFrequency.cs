using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UpgradeShootingFrequency : StatUpgrade<float>
{
    protected override void Awake()
    {
        valuesByLevel = new Dictionary<int, float>
        {
            { 0, 0.7f },
            { 1, 0.6f },
            { 2, 0.5f }
        };
        base.Awake();
    }

    protected override void ApplyStatEffect(float value)
    {
        playerData.playerShootingFrequency = value;
        Debug.Log($"(UpgradeShootingFrequency) Shooting frequency upgraded to {value}");
    }
}