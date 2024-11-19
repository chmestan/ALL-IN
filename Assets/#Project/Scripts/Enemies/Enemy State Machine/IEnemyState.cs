using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState 
{

    public void Enter()
    {
        // when we first enter the state
    }
    public void Update()
    {
        // runs each frame until condition triggering a state change
    }
    public void Exit()
    {
        // when we exit
    }

}
