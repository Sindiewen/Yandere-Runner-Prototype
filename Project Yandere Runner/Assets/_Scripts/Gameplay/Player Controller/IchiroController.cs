using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player Controller for Ichiro
public class IchiroController : InfRunnerController 
{
    /*

    [Header("Player Movement Values")]
    public float MaxMovementSpeed = 5;  // The players movement speed
    
    public bool playerAutoRun = false;          // Weather the player will autorun or not

    public float jumpForce = 5;             // THe force in which the player will jump
	public float fallMultiplier = 2.5f;     // 
    public float lowJumpMultiplier = 2.0f;  //

    [Header("Player Physics")]
    public Transform groundCheck;   // Stores a transform of a Groundcheck object
	public LayerMask whatIsGround;  // Allows choice for choosing what the ground currently is

    [HeaderAttribute("Player Actions")]
    public float dashDistance = 1.0f;       // Sets how far the player will dash
    public float dashCooldown = 5.0f;       // How long the cooldown for the dash will be

    [HeaderAttribute("Platform Spawner")]
    public PlatformSpawnManager platformSpawner;    // Reference to the platform spawn manager


    //UI management
    [HeaderAttribute ("UI")]
    //public UI_Manager UI;                       // Reference to the UI Manager script

    // Private variables

    // Game Input
    //  Keycode input for each of the inputs
    private string _upAction = "UpAction";
    private string _downAction = "DownAction";
    private string _leftAction = "LeftAction";
    private string _rightAction = "RightAction";


    // Player physics
    private bool _facingRight;       	    	// Weather the player is facing right or not
	private bool _isGrounded        = false; 	// If the player is grounded or not
	private bool _isClimbingWall    = false;    // If the player is currently climbing a wall
	private float _groundRadius     = 0.2f;		// The radius of the grounded circle

	// Player Controller
	private float move;				// Stores player movement data
	private bool jump;				// Stores weather the player is jumping or not
	private bool wallClimb;         // Stores weather the player is climbing the wall
    private bool dash;              // Stores weather the player can dash

    // Player Animation
    private Animator anim;          // Reference to the player's Animator component
    private Rigidbody2D rb2D;       // Reference to the player's Rigidbody2D Component

    // Cooldown timers
    private float _dashCooldownStart = 0f;   // Cooldown timer for the player dash
    */



    //////////////////////////////////////////////
    // Class Functions
    //////////////////////////////////////////////

    // UnityEngine Functions //

    private void Awake()
    {
        // Gets the componenets of the player
        _Anim = GetComponent<Animator>();
        _rb2d = GetComponent<Rigidbody2D>();

        // Player will start running after a set period seconds
        Invoke("startRunning", startDelay);
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
    
    // If the player collides with a trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Platform Spawn Logic
        // If player exceeds platform spawner, spawn in new platforms
        if (collision.gameObject.tag == "Platform Spawner")
        {
            for(int i = 0; i < platformSpawner.numOfPlatformsToSpawn; i++)
            {
                // Spawn in a new set of platforms into the scene
                platformSpawner.SpawnPlatform();
            }
        }

        // If player collides with a coin object via Tag
        if(collision.gameObject.tag == "Pickup_Coin")
        {
            // destroy coin object
            Destroy(collision.gameObject);

            // Add 1 to total coin count
            //UI.coinCount += 1;

            //Debug.Log("Total Coin Count: " + UI.coinCount.ToString());
        }
    } 

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Wall Climb Logic
        //  If player isn't touching wall cliimb 
        if (collision.gameObject.tag == "Climb")
        {
            _isClimbingWall = false;
            _rb2d.velocity = new Vector2(0,0);

            // Set animation state to returning to running state
        }
    }
    
    

    // Player Dash
    void playerDash()
    {  
        //Vector2 newDashDistance = new Vector2 (dashDistance, 0);
        // Dashes the plater forward
        //this.transform.Translate(dashDistance * Time.deltaTime, 0, 0);
        //rb2D.AddForce(Vector2.right * dashDistance);

        // TODO: Instead of forcing player forwards, greatly increase player speed temporarily
    }

    // When the player has been caught by Yumi
    void Caught()
    {

    }
}
