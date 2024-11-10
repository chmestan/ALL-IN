using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletHit : MonoBehaviour
{

    [SerializeField] private int damage = 5;

    [SerializeField] private bool debug = false; 
    void OnTriggerEnter2D(Collider2D collider)
    {
        IEnemy enemy = collider.gameObject.GetComponent<IEnemy>();
        ILimit limit = collider.gameObject.GetComponent<ILimit>();

        if (enemy != null)
        {
            if (debug) Debug.Log($"[BulletHit] A bullet has damaged {collider.gameObject} for {damage} damage.");
            enemy.GetHit(damage);
        }

        if (limit != null) 
        {
            gameObject.SetActive(false); 
            if (debug) Debug.Log($"[BulletHit] A bullet has hit {collider.gameObject}");
        }
    }



}
