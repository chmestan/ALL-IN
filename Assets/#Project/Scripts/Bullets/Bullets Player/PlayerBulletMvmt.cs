using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletMvmt : BulletMovement
{
    [SerializeField] private int damage = 5;
    [SerializeField] private bool debug = false; 

    public int Damage
    {
        get => damage;
        set => damage = value;
    }


    // protected override void FixedUpdate()
    // {

    // }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);

        IEnemy enemy = collider.gameObject.GetComponent<IEnemy>();

        if (enemy != null)
        {
            enemy.GetHit(damage);
            gameObject.SetActive(false); 
            if (debug) Debug.Log($"[BulletHit] A bullet has damaged {collider.gameObject} for {damage} damage.");
        }
    }

    // public override void SetDirection(Vector2 newDirection)
    // {

    // }
}
