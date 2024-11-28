using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine 
{

    public EnemyState CurrentEnemyState 
    {
        get;
        set;
    }

    public void Initialize(EnemyState startingState)
    {
        CurrentEnemyState = startingState;
        CurrentEnemyState.EnterState();
    }

    public void ChangeState(EnemyState nextState)
    {
        CurrentEnemyState.ExitState();
        CurrentEnemyState = nextState;
        // Debug.Log($"(EnemyStateMachine) Changing state to {nextState}");
        CurrentEnemyState.EnterState();
    }

    public void Update()
    {
        if (CurrentEnemyState != null)
        {
            CurrentEnemyState.FrameUpdate();
        }
    }

}
