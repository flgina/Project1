using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSnake : MonoBehaviour
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

    public AudioSource audioSource;
    public AudioClip hitSound;

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
        targetDamage = 2;
        
        // fireball
        FireballDamage = 0;
        CurrentFireballDamage = 0;
        targetFireballDamage = 1;

        // attack player
        playerPos = player.GetComponent<Transform>();
        currentPos = GetComponent<Transform>().position;
        rigidbody2d = GetComponent<Rigidbody2D>();

        audioSource = GetComponent<AudioSource>();

    }

    // attack player
    void Update()
    {
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

    // updates damage
    public void UpdateDamage(int damage)
    {
        CurrentDamage += damage;
        PlaySound(hitSound);

        if (targetDamage == CurrentDamage)
        {
            Destroy(gameObject);
            if (Random.value <= 0.7)
            {
                Instantiate(firballPickUp, transform.position, Quaternion.identity);
            }
            if (Random.value <= 0.3)
            {
                Instantiate(health, transform.position, Quaternion.identity);
            }
        }
    }
    public void UpdateFireballDamage(int FireballDamage)
    {
        CurrentFireballDamage += FireballDamage;
        PlaySound(hitSound);

        if (targetFireballDamage == CurrentFireballDamage)
        {
            Destroy(gameObject);
            if (Random.value <= 0.7)
            {
                Instantiate(firballPickUp, transform.position, Quaternion.identity);
            }
            if (Random.value <= 0.3)
            {
                Instantiate(health, transform.position, Quaternion.identity);
            }
        }
    }

    public void PlaySound(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, gameObject.transform.position);
    }
}
