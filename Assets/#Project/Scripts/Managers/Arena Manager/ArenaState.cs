using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaState : MonoBehaviour
{
    public ArenaStateEnum state = ArenaStateEnum.Ongoing;
    PauseGame pauseScript;
    EnemyManager enemyManager;

    void Awake()
    {
        pauseScript = GetComponent<PauseGame>();
        enemyManager = GetComponent<EnemyManager>();
    }

    void Update()
    {
        // check player's remaining health
        // else 
        if (enemyManager.RemainingEnemies <=0)
        {
            state = ArenaStateEnum.Done;
            // if confirm input then change scene
        }
        else
        {
            state = pauseScript.paused? ArenaStateEnum.Paused : ArenaStateEnum.Ongoing;
        }
    }
}
