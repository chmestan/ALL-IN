using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyType0 : EnemyBase
{
    private void Update()
    {
        if (Player.Instance != null)
        {
            agent.SetDestination(Player.Instance.transform.position);
        }
    }
}

