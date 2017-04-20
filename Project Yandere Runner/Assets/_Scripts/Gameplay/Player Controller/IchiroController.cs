using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player Controller for Ichiro
public class IchiroController : InfRunnerController 
{

    // Class variables
    //  Public
    public float distanceTraveled = 0;      // Stores the current distance traveled

    //  Private
    private Vector2 lastPosition;           // Stores the last position

    
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


    private void Start()
    {
        // Initialized the last position as this current position
        //lastPosition = transform.position;
    }

    
    private void Update()
    {
        // Updates the distance traveled between this current position and the last position
        //distanceTraveled += Vector2.Distance(transform.position, lastPosition);

        // Updates the last position with the new position
        //lastPosition = transform.position;
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
