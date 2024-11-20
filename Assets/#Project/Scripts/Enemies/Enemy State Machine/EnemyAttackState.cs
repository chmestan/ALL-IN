using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        agent = enemy.Agent;
        stats = enemy.Stats;
    }
    public override void EnterState()
    {
        base.EnterState();
        if (stats.BulletsPerBurst <= 0) 
        {
            Debug.Log($"(EnemyAttackState) The enemy ({enemy.name}) shouldn't be in attack state, yet it apparently is");
        }
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }


}
