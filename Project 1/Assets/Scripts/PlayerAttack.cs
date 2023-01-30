using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Red Enemy Script
    RedEnemy redEnemy;
    
    // Green Enemy Script
    GreenEnemy greenEnemy;

    // Blue Enemy Script
    BlueEnemy blueEnemy;

    // rigidbody
    Rigidbody2D rigidbody2D;

    // damage
    int damage = 1;

    void Awake()
    {
        // rigidbody
        rigidbody2D = GetComponent<Rigidbody2D>();

        // red enemy
        redEnemy = GameObject.FindObjectOfType<RedEnemy>();

        // green enemy
        greenEnemy = GameObject.FindObjectOfType<GreenEnemy>();

        // blue enemy
        blueEnemy = GameObject.FindObjectOfType<BlueEnemy>();
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2D.AddForce(direction * force);
    }

    void Update()
    {
        if(transform.position.magnitude > 10.0f)
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
            redEnemy.UpdateDamage(1);
            Debug.Log("Collison Red");
        }

        // green
        if (other.CompareTag("green"))
        {
            Destroy(gameObject);
            greenEnemy.UpdateDamage(1);
            Debug.Log("Collison Green");
        }

        // blue
        if (other.CompareTag("blue"))
        {
            Destroy(gameObject);
            blueEnemy.UpdateDamage(1);
            Debug.Log("Collison Blue");
        }
    }
}
