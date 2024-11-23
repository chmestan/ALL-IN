using System.Collections;
using UnityEngine;

public abstract class ShootingEnemy : Enemy
{
    protected int burstsRemaining;

    public override EnemyState GetNextStateAfterRoaming()
    {
        return AttackState; 
    }

    public override EnemyState GetNextStateAfterAttacking()
    {
        if (Random.value < Stats.ChanceToRoam)
        {
            return RoamState;
        }
        return AttackState; 
    }

    // protected virtual IEnumerator ShootingRoutine()
    // {
    //     while (burstsRemaining > 0)
    //     {
    //         for (int i = 0; i < Stats.BulletsPerBurst; i++)
    //         {
    //             ShootBullet();
    //             yield return new WaitForSeconds(Stats.TimeBetweenBullets);
    //         }

    //         burstsRemaining--;

    //         if (burstsRemaining > 0)
    //         {
    //             yield return new WaitForSeconds(Stats.TimeBetweenBursts);
    //         }
    //     }

    //     StateMachine.ChangeState(GetNextStateAfterAttacking());
    // }

    // protected virtual void ShootBullet()
    // {
    //     GameObject bullet = EnemyBulletsPool.SharedInstance.GetPooledObject();
    //     if (bullet != null)
    //     {
    //         bullet.transform.position = transform.position;
    //         bullet.SetActive(true);

    //         Vector2 direction = (Player.Instance.transform.position - bullet.transform.position).normalized;

    //         BulletMovement bulletMovement = bullet.GetComponent<BulletMovement>();
    //         if (bulletMovement != null)
    //         {
    //             bulletMovement.SetDirection(direction);
    //         }
    //     }
    // }
}
