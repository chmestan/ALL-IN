using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        agent = enemy.Agent;
        stats = enemy.Stats;
    }
    public override void EnterState()
    {
        base.EnterState();
        if (stats.BulletsPerBurst <= 0) 
        {
            Debug.Log($"(EnemyAttackState) The enemy ({enemy.name}) shouldn't be in attack state, yet it apparently is");
            return;
        }
        // Start attacking
        enemy.StartCoroutine(AttackCoroutine());
    }
    public override void ExitState()
    {
        base.ExitState();
        enemy.StopAllCoroutines();
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }

    private IEnumerator AttackCoroutine()
    {
        while (true) // continuous atm
        {
            for (int i = 0; i < stats.BulletsPerBurst; i++)
            {
                ShootBullet();
                yield return new WaitForSeconds(stats.TimeBetweenBullets); 
            }

            yield return new WaitForSeconds(stats.TimeBetweenBursts);
        }
    }

    private void ShootBullet()
    {
        GameObject bullet = EnemyBulletsPool.SharedInstance.GetPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = enemy.transform.position; 
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
