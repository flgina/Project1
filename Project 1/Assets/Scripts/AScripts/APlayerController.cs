using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class APlayerController : MonoBehaviour
{
   /* Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    public float speed = 0f;
    public float jumpAmount;

    private Animator animator;

    private float yInput;
    
    // health
    public TextMeshProUGUI healthText;
    public int health;

    public bool facingRight = true; 
    bool isMoving = false; 

    bool isHit = false; 
   // bool isCrouching = false;

    private bool isJumping;
   // private bool isShooting = false;

    Vector2 lookDirection = new Vector2(1,0);

    private Vector3 offset;

    public GameObject playerCharacter;

    // fire projectile
    public GameObject projectilePrefab;
    public GameObject fireballPrefab;
    public Transform Projectilelaunch;
  //  public Transform Crouchlaunch;

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
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

         // health
        health = 10;
        healthText.text = "Health: " + health.ToString();
    
    }

    // Update is called once per frame
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

        //crouch
        yInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetBool("Crouch", true);
            speed = 0f;
           // isCrouching = true; 
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("Crouch", false);
            speed = 3f;
           // isCrouching = false;
        }
    
       //jumping 
         if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
        rigidbody2d.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
        isJumping = true;
        animator.SetBool("Jump", true);
       // isMoving = true;

        }

      // fire projectile
        if(Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        
        }

    
        // check to see if have any fireball
        if (fireballPickUp > 0)
        {
            if(Input.GetKeyUp(KeyCode.V))
            {
                Instantiate(fireballPrefab, Projectilelaunch.position, transform.rotation);
                fireballPickUp -= 1;
                Destroy(GameObject.FindWithTag("Projectile"), 4);
            }
        }
        
        
        if(facingRight)
        {
            offset = new Vector3(0, 0, 0);
        }
        else
        {
            offset = new Vector3 (0, 0, -180);
        }

        if(isHit == true)
        {
            animator.SetTrigger("Hit");
        }
        
    }


    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale; 
        currentScale.x *= -1; 
        gameObject.transform.localScale = currentScale; 

        facingRight = !facingRight; 

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // red crab
        if (other.CompareTag("RedCrab"))
        {
            health -= 1;
            healthText.text = "Health: " + health.ToString();
            rigidbody2d.AddForce(transform.up * 400);
            isHit = true; 
        }

        // green crab
        if (other.CompareTag("GreenCrab"))
        {
            health -= 2;
            healthText.text = "Health: " + health.ToString();
            rigidbody2d.AddForce(transform.up * 400);
            isHit = true;
        }

        // blue crab
        if (other.CompareTag("BlueCrab"))
        {
            health -= 3;
            healthText.text = "Health: " + health.ToString();
            rigidbody2d.AddForce(transform.up * 400);
            isHit = true;
        }

        if (other.CompareTag("BossAttack"))
        {
            health -= 1;
            healthText.text = "Health: " + health.ToString();
            rigidbody2d.AddForce(transform.up * 400);
            isHit = true;
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




    private void OnCollisionStay2D(Collision2D collision)
    {
     
        // fireball pickup
        if (collision.collider.tag == "fireballPickUp")
        {
            fireballPickUp += 1;
            Destroy(collision.gameObject);
        }
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        
        if(other.gameObject.CompareTag("Ground"))
        {
        isJumping = false;
        animator.SetBool("Jump", false);
        }
         // red snake
        if (other.gameObject.CompareTag("RedSnake"))
        {
            health -= 1;
            healthText.text = "Health: " + health.ToString();
            rigidbody2d.AddForce(transform.up * 400);
        }

        // green snake
        if (other.gameObject.CompareTag( "GreenSnake"))
        {
            health -= 2;
            healthText.text = "Health: " + health.ToString();
            rigidbody2d.AddForce(transform.up * 400);
        }

        // blue snake
        if (other.gameObject.CompareTag( "BlueSnake"))
        {
            health -= 3;
            healthText.text = "Health: " + health.ToString();
            rigidbody2d.AddForce(transform.up * 400);
        }

        // boss
        if (other.gameObject.CompareTag("Boss"))
        {
            health -= 1;
            healthText.text = "Health: " + health.ToString();
            rigidbody2d.AddForce(transform.up * 400);
        }
    }
        
    void Launch()
    {
        animator.SetTrigger("Shoot 0");
        GameObject projectileObject = Instantiate(projectilePrefab, Projectilelaunch.position, Quaternion.Euler(offset));
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

   */

}
