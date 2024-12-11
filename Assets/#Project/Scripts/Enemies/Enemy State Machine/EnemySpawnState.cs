using System.Collections;
using UnityEngine;

public class EnemySpawnState : EnemyState
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    public EnemySpawnState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        stats = enemy.Stats;
        spriteRenderer = enemy.GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public override void EnterState()
    {
        base.EnterState();

        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
        enemy.StartCoroutine(FadeInCoroutine(2f)); 
    }

    public override void ExitState()
    {
        base.ExitState();
        enemy.poofSpawn.Play();
        // make sure alpha is set to 1 as we exit the state
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
    }

    private IEnumerator FadeInCoroutine(float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / duration);
                // t going from 0/duration = 0 to duration/duration = 1
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsedTime += Time.deltaTime;

            yield return null; 
        }

        // once fade in is complete, switch to first state
        enemy.StateMachine.ChangeState(enemy.GetStartingState());
    }
}
