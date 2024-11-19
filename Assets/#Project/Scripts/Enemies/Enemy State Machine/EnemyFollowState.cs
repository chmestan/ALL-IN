using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowState : EnemyState
{
    public EnemyFollowState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        agent = enemy.Agent;
        stats = enemy.Stats;
    }
    public override void EnterState()
    {
        base.EnterState();
        Debug.Log($"(EnemyFollowState) {enemy.name} entering Follow State");
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (Player.Instance != null)
        {
            agent.speed = stats.MoveSpeed;
            agent.SetDestination(Player.Instance.transform.position);
        }
    }

}
