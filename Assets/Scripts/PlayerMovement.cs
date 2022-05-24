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
    CapsuleCollider2D myCapsuleCollider;
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;

    void Start()
    {
        //gets access to the players components in unity to edit in the code script
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    //runs all of our constant methods
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

    //gives the player the ability to jump
    void OnJump(InputValue value)
    {

        //allows the player to jump only when they are touching the Ground
        if(!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) {return;}

        if(value.isPressed)
        {
            myRigidbody.velocity += new Vector2 (0f, jumpSpeed);
        }

      
    }

    //gives the player movement and speed on the x axis
    void Run()
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
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
