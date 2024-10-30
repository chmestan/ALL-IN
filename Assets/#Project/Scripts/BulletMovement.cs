using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.gameObject.GetComponent<ILimit>() != null) gameObject.SetActive(false); 
    }
}
