using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : Enemy
{
    public override EnemyState GetStartingState()
    {
        return ChaseState;
    }
    public override EnemyState StateAfterRoaming()
    {
        return ChaseState; 
    }

    public override void Update()
    {
        base.Update();        
    }
}
