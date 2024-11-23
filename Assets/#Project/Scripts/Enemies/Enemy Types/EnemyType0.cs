using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyType0 : ChasingEnemy
{
        public override EnemyState GetStartingState()
        {
            return ChaseState;
        }
}
