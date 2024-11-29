using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BulletMovement : MonoBehaviour
{
    private float speed = 20f;
    private Vector2 direction;
    private float distanceTraveled;
    protected float range = 15f;

    protected virtual void OnEnable()
    {
        distanceTraveled = 0f; 
    }
    protected virtual void FixedUpdate()
    {
        float distanceToTravel = speed * Time.deltaTime;
        transform.Translate(direction * distanceToTravel);
        distanceTraveled += distanceToTravel;

        if (distanceTraveled >= range)
        {
            gameObject.SetActive(false);
            direction = Vector2.zero;
        }

    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
       if (other.gameObject.GetComponent<ILimit>() != null) 
        {
            gameObject.SetActive(false);
            direction = Vector2.zero;
        } 
    }

    public virtual void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

}
