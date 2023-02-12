using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public AudioSource audioSource;
    public AudioClip hitSound;
    public AudioClip chainSound;

    // player controller
    PlayerController playerController;

    // attack player
    public GameObject bossAttack;
    public float fire;
    float nextFire;
    public Transform Launch;

    // animation / movement
    private Animator anim;
    Rigidbody2D rigidbody2d;
    private Transform playerPos;

    public float speed = 3f;
    float horizontal;
    float vertical; 
   public bool facingRight = false;

   void Awake()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();
    }

    private void Start()
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
        playerController = GameObject.FindObjectOfType<PlayerController>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        fire = 10f;
        nextFire = Time.time;

        audioSource = GetComponent<AudioSource>();

    }
    // attack player
    void Update()
    {
        CheckTime();
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if (playerController.transform.position.x < gameObject.transform.position.x && facingRight)
            Flip ();
        if (playerController.transform.position.x > gameObject.transform.position.x && !facingRight)
            Flip ();
    }

    void Flip () {
        //here your flip funktion, as example
        facingRight = !facingRight;
        Vector3 tmpScale = gameObject.transform.localScale;
        tmpScale.x *= -1;
        gameObject.transform.localScale = tmpScale;
    }

    // fire
    void CheckTime()
    {
        if (Time.time > nextFire)
        {
            Instantiate(bossAttack, Launch.position, Quaternion.identity);
            PlaySound(chainSound);
            nextFire = Time.time + fire;
        }
    }

    // updates damage
    public void UpdateDamage(int damage)
    {
        CurrentDamage += damage;
        PlaySound(hitSound);

        if (targetDamage == CurrentDamage)
        {
            Debug.Log("Target: " + targetDamage);
            Debug.Log("Current: " + CurrentDamage);
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
        PlaySound(hitSound);

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

    public void PlaySound(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, gameObject.transform.position);
    }
}
