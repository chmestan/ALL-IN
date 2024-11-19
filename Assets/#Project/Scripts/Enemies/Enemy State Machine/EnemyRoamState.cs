using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRoamState : EnemyState
{
    public EnemyRoamState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        agent = enemy.Agent;
        stats = enemy.Stats;
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log($"(EnemyRoamState) {enemy.name} is roaming.");
        agent.speed = stats.MoveSpeed / 2; // Roam slower than chase

        Vector3 randomDirection = enemy.transform.position + Random.insideUnitSphere * 5f;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, 5f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            // Set a new random destination
            EnterState();
        }
    }
}

