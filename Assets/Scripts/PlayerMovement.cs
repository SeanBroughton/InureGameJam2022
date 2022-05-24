using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
   // Added UnityEngine.InputSystem to use player controls

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    [SerializeField] float runSpeed = 10f;
    
    void Start()
    {
        // Gets access to the players physics system
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    //Runs all of our methods
    void Update()
    {
        Run();
        FlipSprite();
    }

    //creates number values to create player movement ex: (1,0) (0,1)
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    //gives the player movement and speed on the x axis
    void Run()
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
    }

    //makes the player flip facing direction when moving
    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if(playerHasHorizontalSpeed)
        {
             transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
       
    }
}
