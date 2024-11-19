using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBulletMvmt : BulletMovement
{

    // protected override void FixedUpdate()
    // {

    // }


    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);

        if (collider.gameObject.CompareTag("player"))
        {
            Debug.Log("Enemy bullet hit the player!");
        }
    }
}
