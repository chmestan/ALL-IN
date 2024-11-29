using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBulletMvmt : BulletMovement
{
    [SerializeField] private bool debug = false; 
    [SerializeField] private int defaultDamage = 5;
    [SerializeField] private float defaultRange = 5f;

    private int damage;

    protected override void OnEnable() 
    {
        base.OnEnable();
        damage = GlobalManager.Instance.playerData.playerDamage;
        range = GlobalManager.Instance.playerData.playerRange;
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);

        Enemy enemy = collider.gameObject.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.GetHit(damage);
            gameObject.SetActive(false); 
            if (debug) Debug.Log($"[BulletHit] A bullet has damaged {collider.gameObject} for {damage} damage.");
        }
    }

    private void OnDisable()
    {
        damage = defaultDamage;
        range = defaultRange;
    }

}
