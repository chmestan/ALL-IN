using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    private PlayerShoot player;
    private Vector2 direction;

    void FixedUpdate()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       if (other.gameObject.GetComponent<ILimit>() != null) 
        {
            gameObject.SetActive(false);
            direction = Vector2.zero;
        } 
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }
}
