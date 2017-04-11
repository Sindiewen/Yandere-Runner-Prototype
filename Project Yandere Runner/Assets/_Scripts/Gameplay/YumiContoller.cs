using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YumiContoller : MonoBehaviour
{

    // Public Variables

    // Player Movement
    [HeaderAttribute("Movement Values")]
    public float maxMovementSpeed = 5;

    // Jump values
    [HeaderAttribute("Jump Values")]
    public float jumpForce = 5f;            // The force of which the player will jump upwards
    public float fallMultiplier = 2.5f;     //
    public float lowJumpMultiplier = 2.0f;  //

    [HeaderAttribute("Raycast and Physics Values")]
    public float groundRayDistance;     // The distance of the ray point towards the ground
    public float wallRayDistance;       // The distance the ray will travel towards a wall
    public LayerMask whatIsGround;      // Reference to the ground layer
    public LayerMask whatIsWall;        // Reference to the wall
    public Transform groundCheck;       // Stores a transform of a Groundcheck object

    [HeaderAttribute("Raycast Angle")]
    [RangeAttribute(-1.0f, 1.0f)]
    public float pitCheckRayX;      // For the pitcheck Raycast - X
    [RangeAttribute(-1.0f, 1.0f)]
    public float pitCheckRayY;      // For the pitcheck Raycast - Y

    [RangeAttribute(-1.0f, 1.0f)]
    public float wallCheckRayX;     // For the wallcheck Raycast - X
    [RangeAttribute(-1.0f, 1.0f)]
    public float wallCheckRayY;     // For the wallCehck Raycact - Y

    // Private Variables //

    // Where the raycast will start
    private Vector2 _inFrontOfYumiPitCheck;
    private Vector2 _inFrontOfYumiWallCheck;

    // Raycast variable logic	
    private RaycastHit2D _hitGround;        // Raycast - Points if Yumi is touching the ground (pit check)
    private RaycastHit2D _hitWall;          // Raycast - if hiting the wall


    // Jumping Logic
    private Animator _anim;
    private Rigidbody2D _rb2D;

    private bool _jump;
    private bool _isClimbingWall = false;
    private bool _isGrounded;
    private float _groundRadius = 0.2f;


    private void Start()
    {
        // Gets references to the components
        _anim = GetComponent<Animator>();
        _rb2D = GetComponent<Rigidbody2D>();


        // Player starts grounded
        _isGrounded = false;
    }

    private void FixedUpdate()
    {
        // Checks weahter the player is currently grounded or not using a collider circle
        // Returns a bool if grounded or not
        //isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, _groundRadius, whatIsGround);
        _anim.SetBool("OnGround", _isGrounded);

        // automatically moves the player
        _rb2D.velocity = new Vector2(1 * maxMovementSpeed, _rb2D.velocity.y);

        // Sets the float value of the animator's Speed to allow the player animation to play
        // Mathf.abs gets the absolute value --- 1 = 1, -1 = 1
        _anim.SetFloat("Speed", Mathf.Abs(1));
    }

    // Update is called once per frame
    void Update()
    {
        // Defines raycast values for wall and Jump point
        //	This transform's location, where, distance to wall/Jump Point, and what is defined as a wall/Jump Point
        _hitGround = Physics2D.Raycast(this.transform.position, _inFrontOfYumiPitCheck, groundRayDistance, whatIsGround);
        _hitWall = Physics2D.Raycast(this.transform.position, _inFrontOfYumiWallCheck, wallRayDistance, whatIsWall);

        // Sets the angle of the raycast of the raycast
        _inFrontOfYumiPitCheck = new Vector2(pitCheckRayX, pitCheckRayY);
        _inFrontOfYumiWallCheck = new Vector2(wallCheckRayX, wallCheckRayY);

        // Draws ray in the the scene view
        Debug.DrawRay(this.transform.position, _inFrontOfYumiPitCheck, Color.red);
        Debug.DrawRay(this.transform.position, _inFrontOfYumiWallCheck, Color.green);

        // If Yumi's raycastis no longer touching the ground or her ray hit a wall
        // Checks if the raycast is no longer hitting the ground, or is hitting the wall
        if (!_hitGround && _isGrounded || _hitWall && _isGrounded)
        {
            // Automatically jumps
            _rb2D.velocity = Vector2.up * jumpForce;
        }

        // Better Jump:
        if (_rb2D.velocity.y < 0)
        {
            // Player jumping
            _rb2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (_rb2D.velocity.y > 0)
        {
            // Player falling
            _rb2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        // If the player is climbing the wall
        if (_isClimbingWall)
        {
            // Player climbs the wall
            _rb2D.velocity = new Vector2(0, 1 * maxMovementSpeed);
        }


        
        
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If this player has collided with a climbable wall
        if (collision.gameObject.tag == "Climb")
        {
            // Player is climbing wall
            _isClimbingWall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Wall Climb Logic
        //  If player isn't touching wall cliimb 
        if (collision.gameObject.tag == "Climb")
        {
            _isClimbingWall = false;
            _rb2D.velocity = new Vector2(0, 0);

            // Set animation state to returning to running state
        }


    }
    
}

/* 
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YumiContoller : MonoBehaviour
{

    // Public Variables

    // Player Movement
    [HeaderAttribute("Movement Values")]
    public float maxMovementSpeed = 5;

    // Jump values
    [HeaderAttribute("Jump Values")]
    public float jumpForce = 5f;            // The force of which the player will jump upwards
    public float fallMultiplier = 2.5f;     //
    public float lowJumpMultiplier = 2.0f;  //

    [HeaderAttribute("Raycast and Physics Values")]
    public float groundRayDistance;     // The distance of the ray point towards the ground
    public LayerMask whatIsGround;      // Reference to the ground layer
    public LayerMask whatIsWall;        // Reference to the wall
    public Transform groundCheck;       // Stores a transform of a Groundcheck object

    [HeaderAttribute("Raycast Angle")]
    [RangeAttribute(-1.0f, 1.0f)]
    public float rayX;
    [RangeAttribute(-1.0f, 1.0f)]
    public float rayY;

    // Private Variables //

    // Where the raycast will start
    private Vector2 _inFrontOfYumi;

    // Raycast variable logic	
    private RaycastHit2D _hitGround;        // Raycast - Points if Yumi is touching the ground


    // Jumping Logic
    private Animator _anim;
    private Rigidbody2D _rb2D;

    private bool _jump;
    private bool _isClimbingWall = false;
    private bool _isGrounded;
    private float _groundRadius = 0.2f;


    private void Start()
    {
        // Gets references to the components
        _anim = GetComponent<Animator>();
        _rb2D = GetComponent<Rigidbody2D>();


        // Player starts grounded
        _isGrounded = false;
    }

    private void FixedUpdate()
    {
        // Checks weahter the player is currently grounded or not using a collider circle
        // Returns a bool if grounded or not
        //isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, _groundRadius, whatIsGround);
        _anim.SetBool("OnGround", _isGrounded);

        // automatically moves the player
        _rb2D.velocity = new Vector2(1 * maxMovementSpeed, _rb2D.velocity.y);

        // Sets the float value of the animator's Speed to allow the player animation to play
        // Mathf.abs gets the absolute value --- 1 = 1, -1 = 1
        _anim.SetFloat("Speed", Mathf.Abs(1));
    }

    // Update is called once per frame
    void Update()
    {
        // Defines raycast values for wall and Jump point
        //	This transform's location, where, distance to wall/Jump Point, and what is defined as a wall/Jump Point
        _hitGround = Physics2D.Raycast(this.transform.position, _inFrontOfYumi, groundRayDistance, whatIsGround);

        // Sets the length of the raycast
        _inFrontOfYumi = new Vector2(rayX, rayY);

        // If Yumi's raycastis no longer touching the ground...

        //  Jump at max height
        //  Have Yumi do her up action



        // Checks if the raycast is no longer hitting the ground, or is hitting the wall
        if (!_hitGround && _isGrounded)// || _hitGround.transform.gameObject.layer == whatIsWall)
        {
            // Automatically jumps
            _rb2D.velocity = Vector2.up * jumpForce;

            
        }

            // Better Jump:
            if (_rb2D.velocity.y < 0)
            {
                // Player jumping
                _rb2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            if (_rb2D.velocity.y > 0)
            {
                // Player falling
                _rb2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }

        // If the player is climbing the wall
        if (_isClimbingWall)
        {
            // Player climbs the wall
            _rb2D.velocity = new Vector2(0, 1 * maxMovementSpeed);
        }


        
        
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If this player has collided with a climbable wall
        if (collision.gameObject.tag == "Climb")
        {
            // Player is climbing wall
            _isClimbingWall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Wall Climb Logic
        //  If player isn't touching wall cliimb 
        if (collision.gameObject.tag == "Climb")
        {
            _isClimbingWall = false;
            _rb2D.velocity = new Vector2(0, 0);

            // Set animation state to returning to running state
        }


    }
    
}
>>>>>>> master
*/