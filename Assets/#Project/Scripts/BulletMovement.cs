using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    private PlayerShoot player;
    private Vector2 direction;

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.gameObject.GetComponent<ILimit>() != null) gameObject.SetActive(false); 
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
    }
}
