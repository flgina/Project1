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

    // player controller
    PlayerController playerController;

    // attack

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
    }

    void Update()
    {
        // snake attack
        if (Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.forward), 3, LayerMask.GetMask("player")))
        {
            Debug.Log("Raycast has hit green snake");
        }
    }

    // updates damage
    public void UpdateDamage(int damage)
    {
        CurrentDamage += damage;
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
}
