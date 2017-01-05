﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

// Player Controller for Ichiro
public class IchiroController : MonoBehaviour
{

    [Header("Player Movement Values")]
    public float MaxMovementSpeed;  // The players movement speed
	public float jumpForce;			// The players jump force

    [Header("Player Physics")]
    public Transform groundCheck;   // Stores a transform of a Groundcheck object
	public LayerMask whatIsGround;

    // Private variables

    // Rewired Values
    private int playerID = 0;       // Stores the Rewired player id for this character
    private Player player;

    // Player physics
    private bool facingRight;       		// Weather the player is facing right or not
	private bool isGrounded    = false; 	// If the player is grounded or not
	private float groundRadius = 0.2f;		// The radius of the grounded circle

	// Player Controller
	private float move;				// Stores player movement data
	private bool jump;				// Stores weather the player is jumping or not

    // Player Animation
    private Animator anim;          // Reference to the player's Animator component
    private Rigidbody2D rb2D;       // Reference to the player's Rigidbody2D Component




    //////////////////////////////////////////////
    // Class Functions
    //////////////////////////////////////////////

    // When the game starts
    void Awake()
    {
        // Ensures the player has been assigned a Rewired set
        player = ReInput.players.GetPlayer(playerID);
    }

    void Start()
    {
        // Gets the componenets of the player
        anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();

        // Ensures the player is defaulted to facing right
        facingRight = true;

        // Defaults the player being grounded to false
        isGrounded = false;
    }

    // Fixed Update - For Physics
    private void FixedUpdate()
    {
		
        // Checks weahter the player is currently grounded or not using a collider circle
        // Returns a bool if grounded or not
        //isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("OnGround", isGrounded);



        // Stores the movement value of the player
        move = Input.GetAxis("Move Horizontal");

        // Moves the player by adding velocity to the player
        // NOTE: Changing 1 to 'move' will allow full movement in 2d. 1 is auto run
		rb2D.velocity = new Vector2(move * MaxMovementSpeed, rb2D.velocity.y);

        // Flips the player in regards to where they're moving and if they're facing right or not
        if (move > 0 && !facingRight || move < 0 && facingRight)
        {
            flip();
        }

        // Sets the float value of the animator's Speed to allow the player animation to play
        // Mathf.abs gets the absolute value --- 1 = 1, -1 = 1
        anim.SetFloat("Speed", Mathf.Abs(move));
    }

	private void Update()
	{
		// Stores weather the player pressed jump that frame
		jump = Input.GetButtonDown("Jump");
        
		// Checks if the player is grounded and is jumping
		if (isGrounded && jump)
		{
            // Player is now jumping

            // Sets animation - player is jumping
			anim.SetBool("OnGround", false);
			rb2D.AddForce(new Vector2(0, jumpForce));
        }


	}


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the player gets caught by Yumi...
        // End game
    }



    // Flips the player sprite so the player can move either direction
    void flip()
    {
        // Sets facing right to the opposite of what it currently is
        facingRight = !facingRight;

        // Stores the local scale of the player
        Vector3 theScale = transform.localScale;

        // Multiplies it by itself by a negitive value to get an opposite of what it is
        theScale.x *= -1;

        // Sets the localscale to what theScale currently is
        transform.localScale = theScale;
    }

    void Jump()
    {

    }

    // When the player has been caught by Yumi
    void Caught()
    {

    }
}
