using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRoamState : EnemyState
{

    private float dist;

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
        dist = stats.RoamingDistance;

        Vector3 randomDirection = enemy.transform.position + Random.insideUnitSphere * dist;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, dist, NavMesh.AllAreas))
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

