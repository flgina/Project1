using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    // speed
    public float speed = 2f;

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

    void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // red crab
        if (other.CompareTag("RedCrab"))
        {
            Destroy(gameObject);
            redCrab.UpdateFireballDamage(1);
        }

        // green crab
        if (other.CompareTag("GreenCrab"))
        {
            Destroy(gameObject);
            greenCrab.UpdateFireballDamage(1);
        }

        // blue crab
        if (other.CompareTag("BlueCrab"))
        {
            Destroy(gameObject);
            blueCrab.UpdateFireballDamage(1);
        }

        // red snake
        if (other.CompareTag("RedSnake"))
        {
            Destroy(gameObject);
            redSnake.UpdateFireballDamage(1);
        }

        // green snake
        if (other.CompareTag("GreenSnake"))
        {
            Destroy(gameObject);
            greenSnake.UpdateFireballDamage(1);
        }

        // blue snake
        if (other.CompareTag("BlueSnake"))
        {
            Destroy(gameObject);
            blueSnake.UpdateFireballDamage(1);
        }

        // boss
        if (other.CompareTag("Boss"))
        {
            Destroy(gameObject);
            boss.UpdateFireballDamage(1);
        }

        // boss attack
        if (other.CompareTag("BossAttack"))
        {
            Destroy(gameObject);
            boss.UpdateDamage(1);
        }
    }
}
