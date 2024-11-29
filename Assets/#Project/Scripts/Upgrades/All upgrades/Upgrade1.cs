using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade1 : Upgrade
{
    public override void ApplyEffect()
    {
        playerData.playerMaxHealth += 5;
    }
}
