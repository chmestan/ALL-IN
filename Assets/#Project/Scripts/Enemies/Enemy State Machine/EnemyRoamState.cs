using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRoamState : EnemyState
{
    private float roamDistance;
    private int roamsRemaining;

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

        roamDistance = stats.RoamingDistance;

        // how many roams is this going to take?
        roamsRemaining = Random.Range(stats.MinRoams, stats.MaxRoams + 1);
        Debug.Log($"(EnemyRoamState) {enemy.name} will roam {roamsRemaining} times.");

        SetRandomDestination();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        // if destination has been reached
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            roamsRemaining--;

            if (roamsRemaining > 0)
            {
                SetRandomDestination();
            }
            else
            {
                enemy.StateMachine.ChangeState(enemy.StateAfterRoaming()); 
            }
        }
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log($"(EnemyRoamState) {enemy.name} is exiting roaming state.");
    }

    private void SetRandomDestination()
    {
        Vector3 randomDirection = enemy.transform.position + Random.insideUnitSphere * roamDistance;
        NavMeshHit hit;
        Debug.DrawLine(enemy.transform.position, randomDirection, Color.red, 1f);


        if (NavMesh.SamplePosition(randomDirection, out hit, roamDistance, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
        else
        {
            Debug.LogWarning($"(EnemyRoamState) {enemy.name} failed to find a valid roaming destination.");
        }
    }

    
}
