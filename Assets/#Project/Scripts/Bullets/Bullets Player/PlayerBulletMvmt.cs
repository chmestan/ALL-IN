using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletMvmt : BulletMovement
{
    [SerializeField] private bool debug = false; 

    // protected override void FixedUpdate()
    // {

    // }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);

        Enemy enemy = collider.gameObject.GetComponent<Enemy>();

        if (enemy != null)
        {
            int damage = GlobalManager.Instance.playerData.PlayerDamage;
            enemy.GetHit(damage);
            gameObject.SetActive(false); 
            if (debug) Debug.Log($"[BulletHit] A bullet has damaged {collider.gameObject} for {damage} damage.");
        }
    }
}
