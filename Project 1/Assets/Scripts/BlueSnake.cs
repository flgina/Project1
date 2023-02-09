using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSnake : MonoBehaviour
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
        targetDamage = 3;
        
        // fireball
        FireballDamage = 0;
        CurrentFireballDamage = 0;
        targetFireballDamage = 2;
    }

    void Update()
    {
        // snake attack
        if (Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.forward), 3, LayerMask.GetMask("player")))
        {
            Debug.Log("Raycast has hit blue snake");
        }
    }

    // updates damage
    public void UpdateDamage(int damage)
    {
        CurrentDamage += damage;
        if (targetDamage == CurrentDamage)
        {
            Destroy(gameObject);
            if (Random.value <= 0.6)
            {
                Instantiate(firballPickUp, transform.position, Quaternion.identity);
            }
            if (Random.value <= 0.4)
            {
                Instantiate(health, transform.position, Quaternion.identity);
            }
        }
    }
    public void UpdateFireballDamage(int FireballDamage)
    {
        CurrentFireballDamage += FireballDamage;
        if (targetFireballDamage == CurrentFireballDamage)
        {
            Destroy(gameObject);
            if (Random.value <= 0.6)
            {
                Instantiate(firballPickUp, transform.position, Quaternion.identity);
            }
            if (Random.value <= 0.4)
            {
                Instantiate(health, transform.position, Quaternion.identity);
            }
        }
    }
}
