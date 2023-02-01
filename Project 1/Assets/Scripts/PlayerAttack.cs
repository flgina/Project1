using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float speed = 2f;
    // Red Enemy Script
    RedEnemy redEnemy;
    
    // Green Enemy Script
    GreenEnemy greenEnemy;

    // Blue Enemy Script
    BlueEnemy blueEnemy;

    // damage
    int damage = 0;

    void Awake()
    {
        // red enemy
        redEnemy = GameObject.FindObjectOfType<RedEnemy>();

        // green enemy
        greenEnemy = GameObject.FindObjectOfType<GreenEnemy>();

        // blue enemy
        blueEnemy = GameObject.FindObjectOfType<BlueEnemy>();
    }

    void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;
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
