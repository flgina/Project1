using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCrab : MonoBehaviour
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

    // set movement
    public float speed = 2f;
    private float startTime;
    private float journeyLength;
    public Transform startMarker;
    public Transform endMarker;

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
        targetDamage = 1;
        
        // fireball
        FireballDamage = 0;
        CurrentFireballDamage = 0;
        targetFireballDamage = 1;

        // movement
        startTime = Time.time;
        journeyLength = Vector2.Distance(startMarker.position, endMarker.position);

        audioSource = GetComponent<AudioSource>();

    }

    void Update()
     {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(startMarker.position, endMarker.position, Mathf.PingPong (fracJourney, 1));
     }

    // updates damage
    public void UpdateDamage(int damage)
    {
        CurrentDamage += damage;
        PlaySound(hitSound);

        if (targetDamage == CurrentDamage)
        {
            Destroy(gameObject);
            if (Random.value <= 0.8)
            {
                Instantiate(firballPickUp, transform.position, Quaternion.identity);
            }
            if (Random.value <= 0.2)
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
            if (Random.value <= 0.8)
            {
                Instantiate(firballPickUp, transform.position, Quaternion.identity);
            }
            if (Random.value <= 0.2)
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
