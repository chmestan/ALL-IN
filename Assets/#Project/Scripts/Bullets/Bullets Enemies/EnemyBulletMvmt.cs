using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBulletMvmt : BulletMovement
{

    public int bulletDmg;
    private PlayerHealth playerHealth;

    [SerializeField] private bool debug = false;

    protected override void OnEnable()
    {
        base.OnEnable();
        playerHealth = Player.Instance.GetComponent<PlayerHealth>();
    }


    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);
        if (collider.gameObject.CompareTag("player"))
        {
            playerHealth.GetHit(bulletDmg);
            if (debug) Debug.Log($"[BulletHit] A bullet has damaged the player for {bulletDmg} damage.");

            PlayParticlesThenDisable();
        }
    }

    private void OnDisable()
    {
        bulletDmg = 0;
    }

}
