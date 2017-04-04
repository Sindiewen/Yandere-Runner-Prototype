using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player Controller for Ichiro
public class IchiroController : MonoBehaviour
{

    [Header("Player Movement Values")]
    public float MaxMovementSpeed = 5;  // The players movement speed
    
    public bool playerAutoRun = false;          // Weather the player will autorun or not

    public float jumpForce = 5;             // THe force in which the player will jump
	public float fallMultiplier = 2.5f;     // 
    public float lowJumpMultiplier = 2.0f;  //

    [Header("Player Physics")]
    public Transform groundCheck;   // Stores a transform of a Groundcheck object
	public LayerMask whatIsGround;  // Allows choice for choosing what the ground currently is

    // Private variables

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

    // UnityEngine Functions //

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
        // Moves the player //

        // Stores the movement value of the player
        move = Input.GetAxis("Move Horizontal");
		
        // Checks weahter the player is currently grounded or not using a collider circle
        // Returns a bool if grounded or not
        //isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("OnGround", isGrounded);

        // Toggles weather the player is automatically running or not
        if (playerAutoRun)
        {
            // automatically moves the player
            rb2D.velocity = new Vector2(1 * MaxMovementSpeed, rb2D.velocity.y); 
            
            // Sets the float value of the animator's Speed to allow the player animation to play
            // Mathf.abs gets the absolute value --- 1 = 1, -1 = 1
            anim.SetFloat("Speed", Mathf.Abs(1));
        }
        else
        {
            // Moves the player by adding velocity to the player
            // NOTE: Changing 1 to 'move' will allow full movement in 2d. 1 is auto run
		    rb2D.velocity = new Vector2(move * MaxMovementSpeed, rb2D.velocity.y);
		
            // Sets the float value of the animator's Speed to allow the player animation to play
            // Mathf.abs gets the absolute value --- 1 = 1, -1 = 1
            anim.SetFloat("Speed", Mathf.Abs(move));
        }
        
        // Flips the player in regards to where they're moving and if they're facing right or not
        if (move > 0 && !facingRight || move < 0 && facingRight)
        {
            flip();
        }
    }

	private void Update()
	{
		// Stores weather the player pressed jump that frame
		jump = Input.GetButton("Jump");
		
         
		// Checks if the player is grounded and is jumping
		if (isGrounded && jump)
		{
            // Player is now jumping - Pushes player upwards
		    rb2D.velocity = Vector2.up * jumpForce; // Calls Jump Functions
        }
		
        
        // Better jump from youtube video
        // Better Jumpng in Unity with Four Lines of Code
        if (rb2D.velocity.y < 0)    // If player is jumping
        {
            // Sets animation state to false
            anim.SetBool("OnGround", false);
            
            // THe player is jumping
            rb2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb2D.velocity.y > 0 && !jump) // If player is falling
        {
            // Player is falling
            rb2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;   
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the player gets caught by Yumi...
        // End game
    }




    // Player Movement Functions


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
    

    // When the player has been caught by Yumi
    void Caught()
    {

    }
}
