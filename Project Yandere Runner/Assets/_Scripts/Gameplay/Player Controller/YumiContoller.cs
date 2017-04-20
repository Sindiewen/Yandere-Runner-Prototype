using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YumiContoller : InfRunnerController
{

    /*
    // Public Variables

    [HeaderAttribute("Raycast and Physics Values")]
    public static float groundRayDistance;     // The distance of the ray point towards the ground
    public static float wallRayDistance;       // The distance the ray will travel towards a wall
    public static LayerMask whatIsWall;        // Reference to the wall

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
    [HideInInspector]
    public static Vector2 _YumiPitRay;
    [HideInInspector]
    public static Vector2 _yumiWallRay;

    // Raycast variable logic	
    [HideInInspector]
    public static RaycastHit2D _hitGround;      // Raycast - Points if Yumi is touching the ground (pit check)
    [HideInInspector]
    public static RaycastHit2D _hitWall;              // Raycast - if hiting the wall
    */
    

    private void Awake()
    {
        // Gets references to the components
        _Anim = GetComponent<Animator>();
        _rb2d = GetComponent<Rigidbody2D>();
        
        // Player will start running after a set time
        Invoke("startRunning", startDelay);
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
            _rb2d.velocity = new Vector2(0, 0);

            // Set animation state to returning to running state
        }
    }

}