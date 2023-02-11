using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    public float speed = 0f;
    public float jumpAmount;

    private Animator animator;

    private float yInput;

    public bool facingRight = true; 
    bool isMoving = false; 
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
    public Transform Crouchlaunch;

    // fireball pick up
    public int fireballPickUp = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    
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
        
    }


    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale; 
        currentScale.x *= -1; 
        gameObject.transform.localScale = currentScale; 

        facingRight = !facingRight; 

    }


    void OnCollisionEnter2D(Collision2D other)
    {
        
        if(other.gameObject.CompareTag("Ground"))
        {
        isJumping = false;
        animator.SetBool("Jump", false);
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


}
