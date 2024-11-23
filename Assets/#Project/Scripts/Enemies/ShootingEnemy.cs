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
        return RoamState;
    }

}
