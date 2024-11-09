using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletHit : MonoBehaviour
{
    [SerializeField] private bool debug = false; 
    void OnTriggerEnter2D(Collider2D collider)
    {
        IDamageable damageable = collider.gameObject.GetComponent<IDamageable>();
        ILimit limit = collider.gameObject.GetComponent<ILimit>();

        if (damageable != null)
        {
            if (debug) Debug.Log($"[BulletHit] A bullet has damaged {collider.gameObject}");
        }

        if (limit != null) 
        {
            gameObject.SetActive(false); 
            if (debug) Debug.Log($"[BulletHit] A bullet has hit {collider.gameObject}");
        }
    }



}
