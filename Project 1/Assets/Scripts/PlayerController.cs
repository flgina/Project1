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

    // crouch
    public float playerHeight = 4.5f;
    public float normalHeight;
    public float crouchHeight;

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

        // move left
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.position += Vector3.left * speed * Time.deltaTime;
        }

        // move right
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.position += Vector3.right * speed * Time.deltaTime;
        }

        // crouch
        if (Input.GetKeyDown(KeyCode.S))
        {
            playerHeight = crouchHeight;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            playerHeight = normalHeight;
        }
    }

    void FixedUpdate()
    {
        // jump
        rigidbody2d.AddForce(new Vector2(horizontal * speed, vertical * speed));
    }

    // check tag and do damage
    private void OnTriggerEnter2D(Collider2D other)
    {
        // red
        if (other.CompareTag("red"))
        {
            health -= 1;
            healthText.text = "Health: " + health.ToString();
        }

        // green
        if (other.CompareTag("green"))
        {
            health -= 2;
            healthText.text = "Health: " + health.ToString();
        }

        // blue
        if (other.CompareTag("blue"))
        {
            health -= 3;
            healthText.text = "Health: " + health.ToString();
        }
    }

    // jump 
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.Space))
            {
                rigidbody2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }
}
