using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyDefaultStateLogic : MonoBehaviour
{

    #region STATE MACHINE
        public EnemyStateMachine StateMachine {get; set;}
        public EnemySpawnState SpawnState {get; set;}
        public EnemyRoamState RoamState {get; set;}
        public EnemyDeadState DeadState {get; set;}
        public EnemyChaseState ChaseState {get; set;}
        public EnemyAttackState AttackState {get; set;}
    #endregion

    // public virtual void OnPlayerDetected()
    // {
    //     StateMachine.ChangeState(ChaseState);
    // }

    // public virtual void OnPlayerLost()
    // {
    //     StateMachine.ChangeState(RoamState);
    // }

    public virtual EnemyState StateAfterRoaming()
    {
        return AttackState; 
    }

    public virtual EnemyState StateAfterAttacking()
    {
        return RoamState; 
    }
}
