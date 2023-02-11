using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
  

      // set normal attack
    public int CurrentDamage;
    public int damage;
    public int targetDamage;

    // player controller
    PlayerMovement playerController;

    // attack player
    public GameObject player;
    private Transform playerPos;
    private Vector2 currentPos;
    public float distance;
    public float speed = 3f;
    float horizontal;
    float vertical;
    Rigidbody2D rigidbody2d;
    public GameObject bossAttack;
    public float fire;
    float nextFire;
    public Transform Launch;

    Animator bossAnim;
    //Rigidbody2D rigidbody2d;

    void Awake()
    {
        playerController = GameObject.FindObjectOfType<PlayerMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // normal attack
        damage = 0;
        CurrentDamage = 0;
        targetDamage = 25;

        // attack player
        playerPos = player.GetComponent<Transform>();
        currentPos = GetComponent<Transform>().position;
        rigidbody2d = GetComponent<Rigidbody2D>();
        fire = 10f;
        nextFire = Time.time;
    }

     // attack player
    void Update()
    {
        CheckTime();
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (Vector2.Distance(transform.position, playerPos.position) < distance)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, currentPos, speed * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        // jump
        rigidbody2d.AddForce(new Vector2(horizontal * speed, vertical * speed));  
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // check ground
        if (collision.collider.tag == "Ground")
        {
            rigidbody2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
        }
    }

    
    // fire
    void CheckTime()
    {
        if (Time.time > nextFire)
        {
            Instantiate(bossAttack, Launch.position, Quaternion.identity);
            nextFire = Time.time + fire;
        }
    }

}
