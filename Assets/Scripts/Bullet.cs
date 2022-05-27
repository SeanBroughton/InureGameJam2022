using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 20f;
    Rigidbody2D myRigidBody;
    PlayerMovement player;
    float xSpeed;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();

        //shoots the bullet in the direction the player is facing
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }

    //gives the bullet horizontal speed
    void Update()
    {
        myRigidBody.velocity = new Vector2 (xSpeed, 0f);
    }

    //destroys the enemy that the bullet hits and then it destroys itself
    void OnTriggerEnter2D( Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }

    //destroys the bullet when hitting a wall or object
    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
}
