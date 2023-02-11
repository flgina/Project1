using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABoss : MonoBehaviour
{
    private Animator anim;
    Rigidbody2D rigidbody2d;
    private Transform playerPos;
    private PlayerMovement player;
   // PlayerMovement playerController;

    public float speed = 3f;
    float horizontal;
    float vertical;
   // public float jumpAmount;

   // bool triggerValue; 
   public bool facingRight = false;

    private void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
      rigidbody2d = GetComponent<Rigidbody2D>();
      //currentPos = GetComponent<Transform>().position;
      anim = GetComponent<Animator>();
   //   bool triggerValue = anim.GetBool("Jump");
    }
    // attack player
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if (player.transform.position.x < gameObject.transform.position.x && facingRight)
            Flip ();
        if (player.transform.position.x > gameObject.transform.position.x && !facingRight)
            Flip ();

    }

    void Flip () {
        //here your flip funktion, as example
        facingRight = !facingRight;
        Vector3 tmpScale = gameObject.transform.localScale;
        tmpScale.x *= -1;
        gameObject.transform.localScale = tmpScale;
    }

    void FixedUpdate()
    {
        // jump
        //rigidbody2d.AddForce(new Vector2(horizontal * speed, vertical * speed));  
    }
   
}
