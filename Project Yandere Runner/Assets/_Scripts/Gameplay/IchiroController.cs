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

    // Game Input
    //  Keycode input for each of the inputs
    private string _upAction = "UpAction";
    private string _downAction = "DownAction";
    private string _leftAction = "LeftAction";
    private string _rightArrow = "RightAction";


    // Player physics
    private bool _facingRight;       	    	// Weather the player is facing right or not
	private bool _isGrounded        = false; 	// If the player is grounded or not
	private bool _isClimbingWall    = false;    // If the player is currently climbing a wall
	private float _groundRadius     = 0.2f;		// The radius of the grounded circle

	// Player Controller
	private float move;				// Stores player movement data
	private bool jump;				// Stores weather the player is jumping or not
	private bool wallClimb;          // Stores weather the player is climbing the wall

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
        _facingRight = true;

        // Defaults the player being grounded to false
        _isGrounded = false;
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
		_isGrounded = Physics2D.OverlapCircle(groundCheck.position, _groundRadius, whatIsGround);
        anim.SetBool("OnGround", _isGrounded);

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
        if (move > 0 && !_facingRight || move < 0 && _facingRight)
        {
            flip();
        }
    }

	private void Update()
	{
        // Function Variables //

		// Player Input definitions
		jump = Input.GetButton(_upAction);          // Player is jumping
        wallClimb = Input.GetButton(_upAction);     // player is climbing wall


        // Jump Logic //

		// Checks if the player is grounded and is jumping
		if (_isGrounded && jump)
		{
            // Player is now jumping - Pushes player upwards
		    rb2D.velocity = Vector2.up * jumpForce; // Jumps upwards
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



        // Wall Climb logic //
        if (_isClimbingWall && wallClimb)
        {
            //Debug.Log("Player climbing wall");
            // automatically moves the player
            rb2D.velocity = new Vector2(0 ,1 * MaxMovementSpeed); 

            //TODO: Set animation state to climbing wall
        }

	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Wall climb logic
        //  If the player has collided with the wall climb object 
        if (collision.gameObject.tag == "Climb")
        {
            // Player is climbing the wall
            _isClimbingWall = true;
        }

        // If the player gets caught by Yumi...
        // End game
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Wall Climb Logic
        //  If player isn't touching wall cliimb 
        if (collision.gameObject.tag == "Climb")
        {
            _isClimbingWall = false;
            rb2D.velocity = new Vector2(0,0);

            // Set animation state to returning to running state
        }
    }




    // Player Movement Functions


    // Flips the player sprite so the player can move either direction
    void flip()
    {
        // Sets facing right to the opposite of what it currently is
        _facingRight = !_facingRight;

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
