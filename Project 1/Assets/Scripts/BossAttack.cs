using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    float speed = 3f;
    Vector2 moveDirection;
    Rigidbody2D rigidbody2d;

    // player script
    PlayerController playerController;

    void Awake()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();
    }

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        moveDirection = (playerController.transform.position - transform.position).normalized * speed;
        rigidbody2d.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name.Equals ("Player"))
        {
            Destroy(gameObject);
        }
    }
}
