using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Rigidbody2D rigidbody2D;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2D.AddForce(direction * force);
    }

    void Update()
    {
        if(transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // red
        if (other.CompareTag("red"))
        {
            Destroy(gameObject);
            Debug.Log("Collison Red");
        }

        // green
        if (other.CompareTag("green"))
        {
            Destroy(gameObject);
            Debug.Log("Collison Green");
        }

        // blue
        if (other.CompareTag("blue"))
        {
            Destroy(gameObject);
            Debug.Log("Collison Blue");
        }
    }
}
