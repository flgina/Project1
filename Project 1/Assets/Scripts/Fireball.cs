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

    void OnCollisionEnter2D(Collision2D collision)
    {
        // boss
        if (collision.gameObject.tag == "Boss")
        {
            Destroy(gameObject);
            boss.UpdateFireballDamage(1);
        }

        // red snake
        if (collision.gameObject.tag == "RedSnake")
        {
            Destroy(gameObject);
            redSnake.UpdateFireballDamage(1);
        }

        // green snake
        if (collision.gameObject.tag == "GreenSnake")
        {
            Destroy(gameObject);
            greenSnake.UpdateFireballDamage(1);
        }

        // blue snake
        if (collision.gameObject.tag == "BlueSnake")
        {
            Destroy(gameObject);
            blueSnake.UpdateFireballDamage(1);
        }

        // red crab
        if (collision.gameObject.tag == "RedCrab")
        {
            Destroy(gameObject);
            redCrab.UpdateFireballDamage(1);
        }

        // green crab
        if (collision.gameObject.tag == "GreenCrab")
        {
            Destroy(gameObject);
            greenCrab.UpdateFireballDamage(1);
        }

        // blue crab
        if (collision.gameObject.tag == "BlueCrab")
        {
            Destroy(gameObject);
            blueCrab.UpdateFireballDamage(1);
        }
    }
}
