using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // set normal attack
    public int CurrentDamage;
    public int damage;
    public int targetDamage;

    // set fireball attack
    public int CurrentFireballDamage;
    public int FireballDamage;
    public int targetFireballDamage;

    // fireball pickup
    public GameObject firballPickUp;

    // life pickup
    public GameObject health;

    // player controller
    PlayerController playerController;

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

    void Awake()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // normal attack
        damage = 0;
        CurrentDamage = 0;
        targetDamage = 25;
        
        // fireball
        FireballDamage = 0;
        CurrentFireballDamage = 0;
        targetFireballDamage = 15;

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

    // jump 
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

    // updates damage
    public void UpdateDamage(int damage)
    {
        CurrentDamage += damage;
        if (targetDamage == CurrentDamage)
        {
            Destroy(gameObject);
            Instantiate(firballPickUp, transform.position, Quaternion.identity);
            Instantiate(health, transform.position, Quaternion.identity);
            Instantiate(firballPickUp, transform.position, Quaternion.identity);
            Instantiate(health, transform.position, Quaternion.identity);
            Instantiate(firballPickUp, transform.position, Quaternion.identity);
            Instantiate(health, transform.position, Quaternion.identity);
            Instantiate(firballPickUp, transform.position, Quaternion.identity);
            Instantiate(health, transform.position, Quaternion.identity);
        }
    }
    public void UpdateFireballDamage(int FireballDamage)
    {
        CurrentFireballDamage += FireballDamage;
        if (targetFireballDamage == CurrentFireballDamage)
        {
            Destroy(gameObject);
            Instantiate(firballPickUp, transform.position, Quaternion.identity);
            Instantiate(health, transform.position, Quaternion.identity);
            Instantiate(firballPickUp, transform.position, Quaternion.identity);
            Instantiate(health, transform.position, Quaternion.identity);
            Instantiate(firballPickUp, transform.position, Quaternion.identity);
            Instantiate(health, transform.position, Quaternion.identity);
            Instantiate(firballPickUp, transform.position, Quaternion.identity);
            Instantiate(health, transform.position, Quaternion.identity);
        }
    }
}
