using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnState : EnemyState
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public EnemySpawnState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        // agent = enemy.Agent;
        stats = enemy.Stats;
        spriteRenderer = enemy.GetComponent<SpriteRenderer>(); 
        originalColor = spriteRenderer.color;
    }
    public override void EnterState()
    {
        base.EnterState();
        spriteRenderer.color = Color.black;
        enemy.StartCoroutine(DurationOfSpawnState());
    }
    public override void ExitState()
    {
        base.ExitState();
        spriteRenderer.color = originalColor;
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }
        private IEnumerator DurationOfSpawnState()
        {
            yield return new WaitForSeconds(1f);
            enemy.StateMachine.ChangeState(enemy.GetStartingState(stats.StartingStateValue));
        }
}
