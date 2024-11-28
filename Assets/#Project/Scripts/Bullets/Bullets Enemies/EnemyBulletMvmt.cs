using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBulletMvmt : BulletMovement
{

    public int bulletDmg;
    PlayerHealth playerHealth;

    private void OnEnable()
    {
        playerHealth = Player.Instance.GetComponent<PlayerHealth>();
    }


    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);
        if (collider.gameObject.CompareTag("player"))
        {
            playerHealth.GetHit(bulletDmg);
            Debug.Log($"[BulletHit] A bullet has damaged the player for {bulletDmg} damage.");
        }
    }

    private void OnDisable()
    {
        bulletDmg = 0;
    }

}
