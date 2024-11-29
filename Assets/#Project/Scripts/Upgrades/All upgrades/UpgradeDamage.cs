using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeDamage : Upgrade
{
    private Dictionary<int, int> damageByLevel = new Dictionary<int, int>
    {
        { 0, 5 },
        { 1, 7 },
        { 2, 10 },
        { 3, 15 }
    };

    protected override void Start()
    {
        base.Start();
        maxLevel = damageByLevel.Count - 1;
    }


    public override void ApplyEffect()
    {
        int damage;
        if (!damageByLevel.ContainsKey(CurrentLevel))
        {
            Debug.LogError($"(Upgrade) Upgrade level not included in dictionary...");
            return;
        }
        else damage = damageByLevel[CurrentLevel];


        playerData.playerDamage = damage;

        Debug.Log($"(UpgradeDamage) Damage upgraded to {playerData.playerDamage} at level {CurrentLevel}");
    }
}
