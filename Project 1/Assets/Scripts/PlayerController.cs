using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // move
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    public float speed = 3.0f;
    public float jumpAmount;
    public bool facingRight = true; 
    bool isMoving = false;
    private bool isJumping; 
    private bool playerInside;
    Vector2 lookDirection = new Vector2(1,0);
 

    private Vector3 offset;

    // crouch
    private float yInput;

    // health
    public TextMeshProUGUI healthText;
    public int health;

    // animator
    private Animator animator;

    // fire projectile
    public GameObject projectilePrefab;
    public GameObject FireballPrefab;
    public GameObject doorClose;
    public GameObject doorOpen;

    public Transform spawnPoint;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    public Transform projectileLaunch;
    

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

        // health
        health = 10;
        healthText.text = "Health: " + health.ToString();
        
    }

    void Update()
    {
        //player movement
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (horizontal != 0)
        {
            rigidbody2d.AddForce(new Vector2(horizontal * speed, 0f));
            isMoving = true;
         
            animator.SetBool("Moving", true);
        }

        else if (horizontal == 0)
        {
            isMoving = false;
           
            animator.SetBool("Moving", false);

            if(isMoving == false && isJumping == false)
            {
               rigidbody2d.velocity = Vector3.zero; 
            }

        }

        if (horizontal > 0 && !facingRight)
        {
            Flip();
        }

        if (horizontal < 0 && facingRight)
        {
            Flip();
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

        //jumping 
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rigidbody2d.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
            isJumping = true;
            animator.SetBool("Jump", true);
        }

        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(2);
        }

        // fire projectile
        if(Input.GetKeyDown(KeyCode.C))
        {
            Launch();
            Destroy(GameObject.FindWithTag("Projectile"), 1);
        }

        // check to see if have any fireball
        if (fireballPickUp > 0)
        {
            if(Input.GetKeyUp(KeyCode.V))
            {
                Launch2();
                fireballPickUp -= 1;
                Destroy(GameObject.FindWithTag("fireball"), 2);
            }
        }

        // projectile flipping
        if(facingRight)
        {
            offset = new Vector3(0, 0, 0);
        }
        else
        {
            offset = new Vector3 (0, 0, -180);
        }

        if (health <= 0)
        {
            SceneManager.LoadScene(4);
        }

        // Gives Null Reference Error Every Frame until player enters level 2 but works as a win condition, should be better way.
        // if (boss.targetDamage <= boss.CurrentDamage || boss.targetDamage <= boss.FireballDamage)
        // {
        //     SceneManager.LoadScene(3);
        // }
 
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale; 
        currentScale.x *= -1; 
        gameObject.transform.localScale = currentScale; 

        facingRight = !facingRight; 

    }

    // jump 
    private void OnCollisionStay2D(Collision2D collision)
    {
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
            animator.SetTrigger("Hit");
            health -= 1;
            healthText.text = "Health: " + health.ToString();
            rigidbody2d.AddForce(transform.up * 400);
        }

        // green snake
        if (collision.gameObject.tag ==  "GreenSnake")
        {
            animator.SetTrigger("Hit");
            health -= 2;
            healthText.text = "Health: " + health.ToString();
            rigidbody2d.AddForce(transform.up * 400);
        }

        // blue snake
        if (collision.gameObject.tag == "BlueSnake")
        {
            animator.SetTrigger("Hit");
            health -= 3;
            healthText.text = "Health: " + health.ToString();
            rigidbody2d.AddForce(transform.up * 400);
        }

        // boss
        if (collision.gameObject.tag == "Boss")
        {
            animator.SetTrigger("Hit");
            health -= 1;
            healthText.text = "Health: " + health.ToString();
            rigidbody2d.AddForce(transform.up * 400);
        }

        // jump
        if(collision.gameObject.tag == "Ground")
        {
            isJumping = false;
            animator.SetBool("Jump", false);
        }

        // red crab
        if (collision.gameObject.tag == "RedCrab")
        {
            animator.SetTrigger("Hit");
            health -= 1;
            healthText.text = "Health: " + health.ToString();
            rigidbody2d.AddForce(transform.up * 400);
        }

        // green crab
        if (collision.gameObject.tag == "GreenCrab")
        {
            animator.SetTrigger("Hit");
            health -= 2;
            healthText.text = "Health: " + health.ToString();
            rigidbody2d.AddForce(transform.up * 400);
        }

        // blue crab
        if (collision.gameObject.tag == "BlueCrab")
        {
            animator.SetTrigger("Hit");
            health -= 3;
            healthText.text = "Health: " + health.ToString();
            rigidbody2d.AddForce(transform.up * 400);
        }

        if (collision.gameObject.tag == "BossAttack")
        {
            animator.SetTrigger("Hit");
            health -= 1;
            healthText.text = "Health: " + health.ToString();
            rigidbody2d.AddForce(transform.up * 400);
        }

        if (collision.gameObject.tag == "WaterPit")
        {
            animator.SetTrigger("Hit");
            health -= 1;
            healthText.text = "Health: " + health.ToString();
            transform.position = spawnPoint.position;
        }

        if (collision.gameObject.tag == "WaterPit1")
        {
            animator.SetTrigger("Hit");
            health -= 1;
            healthText.text = "Health: " + health.ToString();
            transform.position = spawnPoint1.position;

        }

        if (collision.gameObject.tag == "WaterPit2")
        {
            animator.SetTrigger("Hit");
            health -= 1;
            healthText.text = "Health: " + health.ToString();
            transform.position = spawnPoint2.position;

        }

        if (collision.gameObject.tag == "WaterPit3")
        {
            animator.SetTrigger("Hit");
            health -= 1;
            healthText.text = "Health: " + health.ToString();
            transform.position = spawnPoint3.position;
        }
    

       /* // health
        if ((collision.gameObject.tag == "health") && health < 10)
        {
            health += 2;
            healthText.text = "Health: " + health.ToString();
            Destroy(collision.gameObject);
        }

        if ((collision.gameObject.tag == "health") && health >= 10)
        {
            Destroy(collision.gameObject);
        }*/
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
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

         // fireball pickup
        if (other.CompareTag("fireballPickUp") )
        {
            fireballPickUp += 1;
            Destroy(other.gameObject);
        }

        if (other.CompareTag("DoorClose") )
        {
            doorClose.SetActive(false);
            doorOpen.SetActive(true);
        }

        if (other.CompareTag("DoorOpen"))
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("DoorOpen"))
        {
            playerInside = false;
        }
    }
    

    void Launch()
    {
        animator.SetTrigger("Shoot 0");
        GameObject projectileObject = Instantiate(projectilePrefab, projectileLaunch.position, Quaternion.Euler(offset));
        Projectile projectile = projectileObject.GetComponent<Projectile>();
      

      if(!facingRight)
      {
        projectile.Launch(lookDirection, -300);
      }

     else 
      {
        projectile.Launch(lookDirection, 300);
      }
    } 

    void Launch2()
    {
        animator.SetTrigger("Shoot 0");
        GameObject fireballObject = Instantiate(FireballPrefab, projectileLaunch.position, Quaternion.Euler(offset));
        Projectile2 projectile2 = fireballObject.GetComponent<Projectile2>();
      

      if(!facingRight)
      {
        projectile2.Launch(lookDirection, -300);
      }

     else 
      {
        projectile2.Launch(lookDirection, 300);
      }
    } 
    
}
