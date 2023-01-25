using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // move
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    public float speed = 3.0f;

    // health
    public TextMeshProUGUI healthText;
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        // move
        rigidbody2d = GetComponent<Rigidbody2D>();

        // health
        health = 10;
        healthText.text = "Health: " + health.ToString();
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    // check tag and do damage
    void OnCollisonEnter2D(Collision2D collision)
    {
        // red
        if (collision.gameObject.tag == "red")
        {
            Debug.Log("Collision detercted");
            health -= 1;
            healthText.text = "Health: " + health.ToString();
            Destroy(gameObject);
        }

        // green
        if (collision.gameObject.tag == "green")
        {
            health -= 2;
            healthText.text = "Health: " + health.ToString();
            Destroy(gameObject);
        }

        // blue
        if (collision.gameObject.tag == "blue")
        {
            health -= 3;
            healthText.text = "Health: " + health.ToString();
            Destroy(gameObject);
        }
    }
}
