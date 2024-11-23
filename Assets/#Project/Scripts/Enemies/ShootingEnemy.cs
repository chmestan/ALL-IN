using System.Collections;
using UnityEngine;

public abstract class ShootingEnemy : Enemy
{
    protected int burstsRemaining;

    // public override EnemyState StateAfterRoaming()
    // {
    //     return AttackState; 
    // }

    public override EnemyState StateAfterAttacking()
    {
        if (Random.value < Stats.ChanceToRoam)
        {
            return RoamState;
        }
        return AttackState; 
    }

}
