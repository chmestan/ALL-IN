using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyState
{
    public EnemyDeadState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        
    }
    public override void EnterState()
    {
        base.EnterState();
        enemy.GetComponent<Collider2D>().enabled = false;
        enemy.Agent.isStopped = true;
        enemy.Agent.ResetPath();
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
