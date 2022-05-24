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
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        Run();
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
}
