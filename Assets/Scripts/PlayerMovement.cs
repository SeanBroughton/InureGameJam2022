using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
   //added UnityEngine.InputSystem to use player controls

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    float gravityScaleAtStart;
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;

    void Start()
    {
        //gets access to the players components in unity to edit in the code script
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
    }

    //runs all of our constant methods
    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

    //creates number values to create player movement ex: (1,0) (0,1)
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    //gives the player the ability to jump
    void OnJump(InputValue value)
    {

        //allows the player to jump only when they are touching the Ground
        if(!myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) {return;}
       

        if(value.isPressed)
        {
            myRigidbody.velocity += new Vector2 (0f, jumpSpeed);
        }

        if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) {return;}
      
    }

    //gives the player movement and speed on the x axis
    void Run()
    {
    
        Vector2 playerVelocity = new Vector2 (moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
        
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);

        //stops the run animation from playing while on a ladder
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) 
        {
            myAnimator.SetBool("isRunning", false);
            return;
        }
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
    
    //gives the player the ability to use ladders
    void ClimbLadder()
    {
        //stops the player from climbing infinitely by locking climbing to the ladder sprite
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) 
        {
            myRigidbody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("isClimbing", false);
            return;
        }

        Vector2 climbVelocity = new Vector2 (myRigidbody.velocity.x, moveInput.y * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale= 0f;

        //plays the climbing animation when moving on the ladder
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);

    }
}
