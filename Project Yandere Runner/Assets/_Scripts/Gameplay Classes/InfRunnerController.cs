using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfRunnerController : MonoBehaviour 
{

    // Enums
    //  Defines the playmode of the game using enums
    public enum PlayerMode 
    {
        runnerIchiro,
        chaserYumi
    };

    // Public Variables
    [HeaderAttribute("Player Definitions")]
    public PlayerMode curPlayerMode;                // What the player will currently be when played

	[HeaderAttribute("Player Movement")]
	public bool playerAutoRun 		= false;		// Weather player will automatically Run
    public float startDelay         = 0.0f;         // Sets a delay for when the player will start moving
	public float maxMovementSpeed 	= 20.0f;		// The total max movement speed
	
	public float jumpForce 			= 15.0f;		// How much force should be applided for jumping
	public float fallMultiplier 	= 7.5f;			// Jumping, fall multiplier
	public float lowJumpMultiplier 	= 6.0f;			// Jumping, Low jump

    [HeaderAttribute("Player Physics")]
    public Transform groundCheck;                   // Stores a transform of the ground check object
    public LayerMask whatIsGround;                  // Chooses what the ground layer is

    [HeaderAttribute("Platform Spawner")]
    public PlatformSpawnManager platformSpawner;    // Reference to the platform spawn manager

    [HeaderAttribute("Chaser Physics and Raycast")]
    public float groundRayDistance;                 // THe distance of the raycast pointed towards the ground
                                                    // Checks if player is about to hit a gap to jump over
    public float wallRayDistance;                   // THe distance of the raycast pointed forward
                                                    // CHecks if player is about touch a wall to they can initiate a jump
    public LayerMask whatIsWall;                    // Chooses what the wall layer is
    [RangeAttribute(-1.0f, 1.0f)]
    public float pitCheckRayX;                      // For the pitcheck Raycast - X : Defines x angle
    [RangeAttribute(-1.0f, 1.0f)]
    public float pitCheckRayY;                      // For the pitcheck Raycast - Y : Defines y angle

    [RangeAttribute(-1.0f, 1.0f)]
    public float wallCheckRayX;                     // For the wallcheck Raycast - X : defines X Angle
    [RangeAttribute(-1.0f, 1.0f)]
    public float wallCheckRayY;                     // For the wallCehck Raycact - Y : defines Y Angle





    // Protected Variables
    //  Player Movement
    protected bool _canStartRunning     = false;    // Cheacks weather the player can start running or not
    
    //  Player Physics
    protected bool _isClimbingWall      = false;    // Checks weather the player is climbing a wall
    protected Rigidbody2D _rb2d;                    // Stores the players rigidbody 2d component

    // Player Animation Controller
    //  controls the players Animator component
    protected Animator _Anim;




    // Private Variables

    // Game Input //
    //  Keycode inputs for the for player actions
    private string _upAction    = "UpAction";
    private string _downAction  = "DownAction";
    private string _leftAction  = "LeftAction";
    private string _rightAction = "RightAction";

    // Player Controller //
    //  Interfaced with the player input manager
    private float _playerMove;                  // Stores value of if the player is moving or not from the binded key
    private bool _playerJump;                   // Stores bool of if player is currently jumping
    private bool _playerWallClimb;              // Stores bool weather player is climbing a wall
    private bool _playerDash;                   // Stores bool of weather player is dashing 

    // Player Physics //
    // Stores values of the player physics
    private bool _facingRight       = true;     // Checks weather the player is facing right
    private bool _isGrounded        = false;    // Checks weather the player is currently grounded
    private float _groundRadius      = 0.2f;    // The ground radius - how close the player needs to be to the ground to be considered "grounded"
    
    // Player Raycast Logic //
    //  For Yumi, for doing jump logic
    private RaycastHit2D _YumiPitRay;           // Checks if Yumi has hit a pit
    private RaycastHit2D _yumiWallRay;          // Checks if yumi raycast has hit a wall
    
    // Player Raycast angle
    //   For Yumi, The angle of the rays
    private Vector2 _yumiFrontPitRayAngle;      // The angle of the pit ray
    private Vector2 _yumiFrontWallRayAngle;     // The angle of the wall ray    

    
    




    // Class Implementation

	// Use this for initialization
	private void Start ()
    {
        // Initializes values
        _canStartRunning = false;   // Player defaults to being able to cant start running
        _facingRight = true;        // Player defaults to facing right
        _isGrounded = false;        // Player defaults to not being grounded
        _isClimbingWall = false;    // Player defaults to not climbing wall


        // Invoke - Player will start running after a set time
        //Invoke("startRunning", startDelay);
	}



    // Fixed Update - Controls player physics and player movement
    private void FixedUpdate()
    {
        // Initializes the player movement to the horizontal axis
        _playerMove = Input.GetAxis("Horizontal");

        // Cheacks weather the player is currently grounded using a collider circle
        //  Returns a bool of weather it is grounded or not
        //  Uses the layermask to check if it's colliding with the ground layer
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, _groundRadius, whatIsGround);
        // Sets the animator to correspond to the player being on the ground
        _Anim.SetBool("OnGround", _isGrounded);


        // If the player can start running
        if (_canStartRunning)
        {
            // Checks weather the player will be running automatically or not
            if (playerAutoRun)
            {
                // Automatically move the player to the right keeping the y velocity
                _rb2d.velocity = new Vector2(1 * maxMovementSpeed, _rb2d.velocity.y);

                // Sets the animators float value of the player speel to abs of the input
                //  Mathf.abs returns the absolute value of a number - 1 = 1, -1, = 1
                _Anim.SetFloat("Speed", Mathf.Abs(1));
            }
            else
            {
                // Moves the player by adding the velocity to the player in the x axis by the player input move value
                _rb2d.velocity = new Vector2(_playerMove * maxMovementSpeed, _rb2d.velocity.y);

                // Sets the animators float value of the player speel to abs of the input
                //  Mathf.abs returns the absolute value of a number - 1 = 1, -1, = 1
                _Anim.SetFloat("Speed", Mathf.Abs(_playerMove));
            }
        }

        // Flips the player sprite depending if the player is facing left or right corresponding to the movement direction of the player
        if (_playerMove > 0 && !_facingRight || _playerMove < 0 && _facingRight)
        {
            flip();
        }
    }


    // Update is called once per frame
    void Update ()
    {
        // Function Variables //
        //  Defines the player input for each player action
        _playerJump         = Input.GetButton(_upAction);       // Stores the upAction to the player Jump
        _playerWallClimb    = Input.GetButton(_upAction);       // Stores the upAction to the player wall climb
        _playerDash         = Input.GetButton(_rightAction);    // Stores the rightAction to the player dash

        
        // If the player is runnerIchiro
        if (curPlayerMode == PlayerMode.runnerIchiro)
        {
            // Jump Logic //
            //  
            // Implements by adding force upwards and using the Better Jumping in Unity with Four Lines of Code
            // to smooth out the jumping
            // Checks if the player is grounded and jumping
            if (_isGrounded && _playerJump)
            {
                // Player is jumping - adds velocity upwards for the player to jump
                _rb2d.velocity = Vector2.up * jumpForce;

                // Sets animation jump state to false
                _Anim.SetBool("OnGround", false);
            }
            
            // Better jump from youtube
            if (_rb2d.velocity.y < 0) // If the player is jumping
            {
                // The player is currently jumping
                _rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (_rb2d.velocity.y > 0 && !_playerJump)  // Player is no longer jumping/falling
            {
                // Player is falling
                _rb2d.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }



            // Wall Climb Logic
            //
            // If the player is currently holding the up action and is up against the wall
            if (_isClimbingWall && _playerWallClimb)
            {
                // Moves the player upwards
                _rb2d.velocity = new Vector2(0, 1 * maxMovementSpeed);
                
                // TODO: Give the player a climbing animation and set the animation to slow the player climbing
            }


            // Player dash logic //
            //
            // If the player presses the right action, they can dash
            if (_playerDash)
            {
                // Player is now dashing
                
            }
        }
        // If the playermode is chaserYumi
        else if (curPlayerMode == PlayerMode.chaserYumi)
        {
            // Defines raycast values for wall the jump point
            //  This transform's location, where, distance to wall/jump point, and what is defined as wall
            _YumiPitRay = Physics2D.Raycast(this.transform.position, _yumiFrontPitRayAngle, groundRayDistance, whatIsGround);
            _yumiWallRay = Physics2D.Raycast(this.transform.position, _yumiFrontWallRayAngle, wallRayDistance, whatIsWall);

            //TODO: Get the yumi controller specific values out of this class and move it all to YumiController.cs
            //  Ensure the variables you want ot use are public static

            // Sets the angle of the raycast
            _yumiFrontPitRayAngle = new Vector2(pitCheckRayX, pitCheckRayY);
            _yumiFrontWallRayAngle = new Vector2(wallCheckRayX, wallCheckRayY);

            // Draws the rays in the scene view
            Debug.DrawRay(this.transform.position, _yumiFrontPitRayAngle, Color.red);
            Debug.DrawRay(this.transform.position, _yumiFrontWallRayAngle, Color.green);
            
            // If yumi's raycast is no longer touching the ground (raycast is hitting nothing, like a pit)
            //  or her front ray has hit a wall, Yumi will jump.
            if (!_YumiPitRay && _isGrounded || _yumiWallRay && _isGrounded)
            {
                // Yumi automatically jumps
                _rb2d.velocity = Vector2.up * jumpForce;

                // Changes animation state to jump
                _Anim.SetBool("OnGround", false);
            }

            // Better jump from youtube
            if (_rb2d.velocity.y < 0) // If the player is jumping
            {
                // The player is currently jumping
                _rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (_rb2d.velocity.y > 0 && !_playerJump)  // Player is no longer jumping/falling
            {
                // Player is falling
                _rb2d.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }

            // If the player is climbing the wall
            if (_isClimbingWall)
            {
                // Player climbs wall
                _rb2d.velocity = new Vector2 (0, 1 * maxMovementSpeed);
            }
        }
	}




    // Player movement functions //

    protected void startRunning()
    {
        // Player will start running now
        _canStartRunning = true;
    }

    private void flip()
    {
        // Sets _facingRight to the opposite of what it currently is
        _facingRight = !_facingRight;

        // Stores the local scale of the player
        Vector3 theScale = transform.localScale;

        // Multiplies the local scale by itself by a neitive value to get an opposite of what it is
        theScale.x *= -1;

        // Sets the localscale to what theScale currently is
        transform.localScale = theScale;
    }
}