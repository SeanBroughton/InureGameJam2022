using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
   //added UnityEngine.InputSystem to use player controls

    Vector2 moveInput;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    float gravityScaleAtStart;
    bool isAlive = true;
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);

    void Start()
    {
        //gets access to the players components in unity to edit in the code script
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidBody.gravityScale;
    }

    //runs all of our constant methods
    void Update()
    {
        if(!isAlive) {return;}

        Run();
        FlipSprite();
        ClimbLadder();
        Die();    
    }

    //creates number values to create player movement ex: (1,0) (0,1)
    void OnMove(InputValue value)
    {
        if(!isAlive) {return;}

        moveInput = value.Get<Vector2>();
    }

    //gives the player the ability to jump
    void OnJump(InputValue value)
    {
        if(!isAlive) {return;}

        //allows the player to jump only when they are touching the Ground
        if(!myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) {return;}
       
        if(value.isPressed)
        {
            myRigidBody.velocity += new Vector2 (0f, jumpSpeed);
        }

        //stops the player from wall running
        if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) {return;}
      
    }

    //gives the player movement and speed on the x axis
    void Run()
    {
    
        Vector2 playerVelocity = new Vector2 (moveInput.x * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
        
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
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
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if(playerHasHorizontalSpeed)
        {
             transform.localScale = new Vector2 (Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
       
    }
    
    //gives the player the ability to use ladders
    void ClimbLadder()
    {
        //stops the player from climbing infinitely by locking climbing to the ladder sprite
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) 
        {
            myRigidBody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("isClimbing", false);
            return;
        }

        Vector2 climbVelocity = new Vector2 (myRigidBody.velocity.x, moveInput.y * climbSpeed);
        myRigidBody.velocity = climbVelocity;
        myRigidBody.gravityScale= 0f;

        //plays the climbing animation when moving on the ladder
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);

    }

    void Die()
    {
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigidBody.velocity = deathKick;
        }
    }
}
