using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyStateMachine 
{

    public IEnemyState CurrentState 
    { 
        get;
        private set;
    }

    public EnemyIdleState idleState;
    public EnemyShootState shootState;
    public EnemyFollowState followState;
    public EnemyRetreatState retreatState;

    public EnemyStateMachine(EnemyBase enemy) // we need the constructors since we don't inherit from MonoBehavior
    {
        idleState = new EnemyIdleState(enemy);
        followState = new EnemyFollowState(enemy);
        retreatState = new EnemyRetreatState(enemy);
        shootState = new EnemyShootState(enemy);
    }


    public void Initialize (IEnemyState startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }

    public void TransitionTo(IEnemyState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        nextState.Enter();
    }


    public void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.Update();
        }
    }

}
