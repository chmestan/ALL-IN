using System.Collections;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private int burstsRemaining;

    public EnemyAttackState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        agent = enemy.Agent;
        stats = enemy.Stats;
    }

    public override void EnterState()
    {
        base.EnterState();

        // how many bursts is this attack going to be?
        burstsRemaining = Random.Range(stats.MinBursts, stats.MaxBursts + 1);

        if (stats.BulletsPerBurst <= 0)
        {
            Debug.LogWarning($"(EnemyAttackState) {enemy.name} has no bullets per burst set.");
            return;
        }

        enemy.StartCoroutine(AttackRoutine());
    }

    public override void ExitState()
    {
        base.ExitState();
        enemy.StopAllCoroutines(); 
    }

    private IEnumerator AttackRoutine()
    {
        while (burstsRemaining > 0)
        {
            if (enemy.attackWarning != null) enemy.attackWarning.Play();

            yield return new WaitForSeconds(0.5f);

            for (int i = 0; i < stats.BulletsPerBurst; i++)
            {
                ShootBullet();
                enemy.audioManager.PlaySFX(enemy.enemyShootAudioClip);
                yield return new WaitForSeconds(stats.TimeBetweenBullets);
            }

            burstsRemaining--;

            if (burstsRemaining > 0)
            {
                yield return new WaitForSeconds(stats.TimeBetweenBursts);
            }
        }

    // Transition to the next state after attacking
        enemy.StateMachine.ChangeState(enemy.StateAfterAttacking());
    }
    private void ShootBullet()
    {
        GameObject bullet = EnemyBulletsPool.SharedInstance.GetPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = enemy.transform.position; 
            EnemyBulletMvmt enemyBulletStats = bullet.GetComponent<EnemyBulletMvmt>();
            if (enemyBulletStats == null) Debug.LogError("(EnemyAttackStats) Couldn't find EnemyBulletMvmt component on bullet");
            else enemyBulletStats.bulletDmg = stats.BulletDamage;
            bullet.SetActive(true);

            Vector2 direction = (playerTransform.position - bullet.transform.position).normalized;

            BulletMovement bulletMovement = bullet.GetComponent<BulletMovement>();
            if (bulletMovement != null)
            {
                bulletMovement.SetDirection(direction);
            }
        }
    }
}
