using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public abstract class EnemyState 
{
    protected Enemy enemy;
    protected EnemyStateMachine enemyStateMachine;
    protected NavMeshAgent agent;
    protected EnemyStats stats;

    protected Transform playerTransform;


    public EnemyState(Enemy enemy, EnemyStateMachine enemyStateMachine)
    {
        this.enemy = enemy;
        this.enemyStateMachine = enemyStateMachine;
        playerTransform = Player.Instance.transform;
    }

    public virtual void EnterState()
    {

    }
    public virtual void ExitState()
    {

    }
    public virtual void FrameUpdate()
    {

    }


}
