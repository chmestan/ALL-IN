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
    protected ParticleSystem particles;
    protected SpriteRenderer spriteRenderer;
    protected new Collider2D collider2D;
    private bool isDisabling; 

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
        particles = GetComponentInChildren<ParticleSystem>();
    }

    protected virtual void OnEnable()
    {
        distanceTraveled = 0f;
        spriteRenderer.enabled = true;
        collider2D.enabled = true;
        isDisabling = false; 
    }

    protected virtual void FixedUpdate()
    {
        if (isDisabling) return; 

        float distanceToTravel = speed * Time.deltaTime;
        transform.Translate(direction * distanceToTravel);
        distanceTraveled += distanceToTravel;

        if (distanceTraveled >= range)
        {
            PlayParticlesThenDisable();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (isDisabling) return; 

        if (other.gameObject.GetComponent<ILimit>() != null)
        {
            PlayParticlesThenDisable();
        }
    }

    protected void PlayParticlesThenDisable()
    {
        if (isDisabling) return;

        isDisabling = true; 

        if (particles != null) particles.Play();
        else Debug.LogWarning("(BulletMovement) Couldn't find particles");

        spriteRenderer.enabled = false;
        collider2D.enabled = false;

        direction = Vector2.zero;

        StartCoroutine(DeactivateAfterParticles());
    }

    protected IEnumerator DeactivateAfterParticles()
    {
        yield return new WaitForSeconds(particles.main.duration);

        gameObject.SetActive(false);
    }

    public virtual void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }
}
