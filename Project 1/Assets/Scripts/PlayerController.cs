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
    private Vector2 normalHeight;
    public float crouchHeight;
    private float yInput;

    // health
    public TextMeshProUGUI healthText;
    public int health;

    // animator
    Animator animator;

    // animation
    public GameObject Player;

    // fire projectile
    public GameObject ProjectilePrefab;
    public GameObject FireballPrefab;
    public Transform Launch;

    // fireball pick up
    public int fireballPickUp = 0;

    // Red Enemy Script
    RedCrab redCrab;
    RedSnake redSnake;
    
    // Green Enemy Script
    GreenCrab greenCrab;
    GreenSnake greenSnake;

    // Blue Enemy Script
    BlueCrab blueCrab;
    BlueSnake blueSnake;

    // Boss Script
    Boss boss;

    Vector2 lookDirection = new Vector2(1,0);

    void Awake()
    {
        // red enemy
        redSnake = GameObject.FindObjectOfType<RedSnake>();
        redCrab = GameObject.FindObjectOfType<RedCrab>();

        // green enemy
        greenSnake = GameObject.FindObjectOfType<GreenSnake>();
        greenCrab = GameObject.FindObjectOfType<GreenCrab>();

        // blue enemy
        blueSnake = GameObject.FindObjectOfType<BlueSnake>();
        blueCrab = GameObject.FindObjectOfType<BlueCrab>();

        // boss enemy
        boss = GameObject.FindObjectOfType<Boss>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // move
        rigidbody2d = GetComponent<Rigidbody2D>();

        // animator
        animator = GetComponent<Animator>();

        // crouch
        normalHeight = transform.localScale;

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
            speed = 3f;
            gameObject.transform.position += Vector3.left * speed * Time.deltaTime;
        }

        // move right
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.position += Vector3.right * speed * Time.deltaTime;
        }

        // crouch
        yInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetBool("Crouch", true);
            speed = 0f;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("Crouch", false);
            speed = 3f;
        }

        // fire projectile
        if(Input.GetKeyUp(KeyCode.C))
        {
            Instantiate(ProjectilePrefab, Launch.position, transform.rotation);
            Destroy(GameObject.FindWithTag("Projectile"), 2);
        }

        // check to see if have any fireball
        if (fireballPickUp > 0)
        {
            if(Input.GetKeyUp(KeyCode.V))
            {
                Instantiate(FireballPrefab, Launch.position, transform.rotation);
                fireballPickUp -= 1;
                Destroy(GameObject.FindWithTag("fireball"), 4);
            }
        }
    }

    void FixedUpdate()
    {
        // jump
        rigidbody2d.AddForce(new Vector2(horizontal * speed, vertical * speed));  
    }

    // health
    private void OnTriggerEnter2D(Collider2D other)
    {
        // red crab
        if (other.CompareTag("RedCrab"))
        {
            health -= 1;
            healthText.text = "Health: " + health.ToString();
            rigidbody2d.AddForce(transform.up * 400);
        }

        // green crab
        if (other.CompareTag("GreenCrab"))
        {
            health -= 2;
            healthText.text = "Health: " + health.ToString();
            rigidbody2d.AddForce(transform.up * 400);
        }

        // blue crab
        if (other.CompareTag("BlueCrab"))
        {
            health -= 3;
            healthText.text = "Health: " + health.ToString();
            rigidbody2d.AddForce(transform.up * 400);
        }

        if (other.CompareTag("BossAttack"))
        {
            health -= 1;
            healthText.text = "Health: " + health.ToString();
            rigidbody2d.AddForce(transform.up * 400);
        }

        // health
        if (other.CompareTag("health") && health < 10)
        {
            health += 2;
            healthText.text = "Health: " + health.ToString();
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("health") && health >= 10)
        {
            other.gameObject.SetActive(false);
        }
    }

    // jump 
    private void OnCollisionStay2D(Collision2D collision)
    {
        // check ground
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.Space))
            {
                rigidbody2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }

        // fireball pickup
        if (collision.collider.tag == "fireballPickUp")
        {
            fireballPickUp += 1;
            Destroy(collision.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // red snake
        if (collision.gameObject.tag == "RedSnake")
        {
            health -= 1;
            healthText.text = "Health: " + health.ToString();
            rigidbody2d.AddForce(transform.up * 400);
        }

        // green snake
        if (collision.gameObject.tag ==  "GreenSnake")
        {
            health -= 2;
            healthText.text = "Health: " + health.ToString();
            rigidbody2d.AddForce(transform.up * 400);
        }

        // blue snake
        if (collision.gameObject.tag == "BlueSnake")
        {
            health -= 3;
            healthText.text = "Health: " + health.ToString();
            rigidbody2d.AddForce(transform.up * 400);
        }

        // boss
        if (collision.gameObject.tag == "Boss")
        {
            health -= 1;
            healthText.text = "Health: " + health.ToString();
            rigidbody2d.AddForce(transform.up * 400);
        }
    }
}
