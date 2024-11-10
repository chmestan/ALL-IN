using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public interface IEnemy
{
    void GetHit(int damage);
    void Die();
    void Attack();
}

