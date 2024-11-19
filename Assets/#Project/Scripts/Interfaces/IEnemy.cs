using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public interface IEnemy
{
    public void GetHit(int damage);
    private void Die()
    {

    }

    protected virtual void Attack()
    {

    }
    protected virtual void Idle()
    {

    }
    protected virtual void Follow()
    {
        
    }
    protected virtual void Retreat()
    {

    }

}

