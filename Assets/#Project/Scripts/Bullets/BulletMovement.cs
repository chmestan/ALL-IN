using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BulletMovement : MonoBehaviour
{
    protected Vector2 direction;
    private float distanceTraveled;
    protected float range = 15f;
    protected float speed = 13f;
    protected ParticleSystem collisionParticles;
    protected SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collisionParticles = GetComponentInChildren<ParticleSystem>();
    }
    private void Start()
    {
    }
    protected virtual void OnEnable()
    {
        distanceTraveled = 0f; 
        spriteRenderer.enabled = true;
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
            if (collisionParticles != null) collisionParticles.Play();
            else Debug.LogWarning("(BulletMovement) Couldn't find particles");

            spriteRenderer.enabled = false;
            direction = Vector2.zero;
            StartCoroutine(DeactivateAfterParticles());
        }
    }

    protected IEnumerator DeactivateAfterParticles()
    {
        yield return new WaitForSeconds(collisionParticles.main.duration);

        gameObject.SetActive(false);
    }

    public virtual void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

}
