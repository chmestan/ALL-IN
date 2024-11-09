using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletHit : MonoBehaviour
{

    [SerializeField] private bool debug = false; 
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (debug) Debug.Log($"{collider.name}");
    }
}
