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
    RedEnemy redEnemy;
    
    // Green Enemy Script
    GreenEnemy greenEnemy;

    // Blue Enemy Script
    BlueEnemy blueEnemy;

    void Awake()
    {
        // red enemy
        redEnemy = GameObject.FindObjectOfType<RedEnemy>();

        // green enemy
        greenEnemy = GameObject.FindObjectOfType<GreenEnemy>();

        // blue enemy
        blueEnemy = GameObject.FindObjectOfType<BlueEnemy>();
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

        // checks to see if enemy died
        if (redEnemy.CurrentDamage == redEnemy.targetDamage)
        {
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
}
